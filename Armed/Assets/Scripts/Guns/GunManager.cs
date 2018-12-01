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
    private float reloadProgressComplete = 1;
    private int maxBullets;
    public int numBullets;
    public int shotsPerVolley;
    private bool spreadMode = false;
    private float playerWidth;

    private float timeBetweenVolleys = 0.1f;
    private float timeSinceLastVolley;
    public int currentGun;

    private PlayerHealth health;
    private PlayerMovement movement;
    private int formNum = 1;

    // Use this for initialization
    void Start () {
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<PlayerMovement>();
        currentGun = 0;
        timeSinceLastVolley = timeBetweenVolleys;
        foreach (Transform child in gunHolder.transform)
        {
            numGuns += 1;
            maxBullets += child.gameObject.GetComponent<Gun>().numBullets;
        }
        numBullets = maxBullets;
        reloadProgress = 0;
        playerWidth = this.GetComponent<Renderer>().bounds.size.x;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(maxBullets);
        if (Input.GetButton("Fire1") && timeSinceLastVolley >= timeBetweenVolleys && numBullets > 0 && numGuns > 0)
        {
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
        if (Input.GetButtonDown("ChangeFiringMode"))
        {
            toggleFiringMode();
        }
        timeSinceLastVolley += Time.deltaTime;
        if (currentGun >= numGuns) currentGun = numGuns - 1;
        if (currentGun < 0) currentGun = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
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
        if (numGuns == 5 || numGuns == 10 || numGuns == 15 || numGuns == 20)
        {
            formNum++;
            health.calculateHealth(formNum);
            movement.calculateMovementSpeed(formNum);
            health.scaleUp();
        }
    }

    bool dropGun() {
        if (numGuns < 1) {
            return false;
        }

        Transform gun = gunHolder.transform.GetChild(0);
        gun.GetComponent<Rigidbody2D>().isKinematic = false;
        gun.transform.localScale = new Vector3(10, 2, 2);
        numGuns--;
        gun.GetComponent<Rigidbody2D>().velocity = this.transform.up * 50;
        gun.parent = null;
        maxBullets -= gun.GetComponent<Gun>().numBullets;
        numBullets -= (maxBullets / gun.GetComponent<Gun>().numBullets);
        if (numBullets > maxBullets) numBullets = maxBullets;

        if (numGuns == 4 || numGuns == 9 || numGuns == 14 || numGuns == 19)
        {
            formNum--;
            health.calculateHealth(formNum);
            movement.calculateMovementSpeed(formNum);
            health.scaleDown();
        }
        return true;
    }

    void toggleFiringMode() {
        spreadMode = !spreadMode;
        float gunAreaWidth = playerWidth - 0.35f;
        float distBetween = gunAreaWidth / (numGuns - 1);
        float position = -gunAreaWidth / 2;
        if (numGuns == 1) {
            position = 0;
        }
        float angle;
        float angleDiffBetween;
        if (spreadMode) {
            angle = 110;
            angleDiffBetween = 40 / (numGuns - 1);
        } else {
            angle = 90;
            angleDiffBetween = 0;
        }
        foreach (Transform child in gunHolder.transform)
        {
            Vector3 pos = child.gameObject.GetComponent<Gun>().transform.localPosition;
            child.gameObject.GetComponent<Gun>().transform.localPosition = new Vector3(position, 0.5f, pos.z);
            child.gameObject.GetComponent<Gun>().transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            position += distBetween;
            angle -= angleDiffBetween;
        }
    }

    public int getCurBullets(){return numBullets;}
	public int getMaxBullets(){return maxBullets;}
}
