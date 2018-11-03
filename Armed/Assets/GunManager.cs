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
    private int numGuns;
    private float lastFireTime;
    private float reloadProgress;
    private float reloadProgressComplete = 3;
    private int maxBullets;
    private int numBullets;
    private int shotsPerVolley;

	// Use this for initialization
	void Start () {
        lastFireTime = 0;
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
        if (Input.GetButton("Fire1"))
        {
            foreach (Transform child in gunHolder.transform) {
                if (numBullets <= 0)
                {
                    break;
                }
                if (child.GetComponent<Gun>().Shoot())
                {
                    numBullets--;
                }
            }
            lastFireTime = Time.fixedTime;
            reloadProgress = 0;
        }
        else
        {
            reloadProgress += Time.deltaTime;
        }

        if (reloadProgress >= reloadProgressComplete) {
            numBullets = maxBullets;
        }

        if (Input.GetButtonDown("DropGun")) {
            dropGun();
        }
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
        gun.transform.localPosition = new Vector3(Random.Range(-.5f, .5f), .5f, 0);
        gun.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90 + Random.Range(-gunDistAngle / 2, gunDistAngle / 2)));
        maxBullets += gun.GetComponent<Gun>().numBullets;
        numBullets += gun.GetComponent<Gun>().numBullets;
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
        return true;
    }
}
