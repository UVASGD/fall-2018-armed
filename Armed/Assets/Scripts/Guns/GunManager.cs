using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour {

    public int maxHeat;
    public float currHeat;
    public float heatDecreaseRatePerGun;
    public float heatIncreaseRate;
    public float gunDistAngle;
    public GameObject gunHolder;
    public int numGuns;
    public float reloadProgress;
    private float reloadProgressComplete = 0;
    private int maxBullets;
    public int numBullets;
    public int shotsPerVolley;

    private float timeBetweenVolleys = 0.1f;
    private float timeSinceLastVolley;
    public int currentGun;


    // Use this for initialization
    void Start () {
        currentGun = 0;
        timeSinceLastVolley = timeBetweenVolleys;
        foreach (Transform child in gunHolder.transform)
        {
            numGuns += 1;
            maxBullets += child.gameObject.GetComponent<Gun>().numBullets;
        }
        numBullets = maxBullets;
        reloadProgress = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(maxBullets);
        if (Input.GetButton("Fire1") && timeSinceLastVolley >= timeBetweenVolleys && numBullets > 0)
        {
            int numGuns = gunHolder.transform.childCount;
            for (int i = 0; i < shotsPerVolley; i++) {
                if (numBullets <= 0)
                {
                    break;
                }
                Transform child = gunHolder.transform.GetChild(currentGun);
                if (child.GetComponent<Gun>().Shoot())
                {
                    numBullets--;
                }
            }
            timeSinceLastVolley = 0;
            reloadProgress = 0;
            currentGun = (currentGun + 1) % numGuns;
        }
        else
        {
            reloadProgress += Time.deltaTime;
            if (reloadProgress >= reloadProgressComplete)
            {
                numBullets = maxBullets;
            }
        }

        if (Input.GetButtonDown("DropGun")) {
            dropGun();
        }
        timeSinceLastVolley += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);
        if (other.gameObject.tag == "pistol")
        {
            pickUpGun(other.gameObject);
        }
    }

    void pickUpGun(GameObject gun) {
        if (gun.transform.IsChildOf(this.transform)) {
            return;
        }
        numGuns++;
        gun.transform.parent = gunHolder.transform;
        gun.GetComponent<Rigidbody2D>().isKinematic = true;
        gun.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        gun.transform.localPosition = new Vector3(Random.Range(-.5f, .5f), .5f, 0);
        gun.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90 + Random.Range(-gunDistAngle / 2, gunDistAngle / 2)));
        maxBullets += gun.GetComponent<Gun>().numBullets;
        numBullets += gun.GetComponent<Gun>().numBullets;
        if (numBullets > maxBullets) numBullets = maxBullets;
    }

    bool dropGun() {
        if (numGuns < 1) {
            return false;
        }

        Transform gun = gunHolder.transform.GetChild(0);
        gun.GetComponent<Rigidbody2D>().isKinematic = false;
        numGuns--;
        gun.GetComponent<Rigidbody2D>().velocity = this.transform.up * 50;
        gun.parent = null;
        maxBullets -= gun.GetComponent<Gun>().numBullets;
        numBullets -= (maxBullets / gun.GetComponent<Gun>().numBullets);
        if (numBullets > maxBullets) numBullets = maxBullets;
        return true;
    }

	public int getCurBullets(){return numBullets;}
	public int getMaxBullets(){return maxBullets;}
}
