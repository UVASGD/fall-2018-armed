using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour {

    public int maxHeat;
    public float currHeat;
    public int numGuns;
    public float heatDecreaseRatePerGun;
    public float heatIncreaseRate;
    public float gunDistAngle;
    public GameObject gunHolder;
    private float lastFireTime;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1"))
        {
            currHeat = Mathf.Clamp(currHeat - numGuns * heatDecreaseRatePerGun * Time.deltaTime, 0, maxHeat);
            //Fire the gun from a specific location
            foreach (Transform child in gunHolder.transform) {
                child.GetComponent<Gun>().Shoot();
            }
        }
        else
        {
            currHeat = Mathf.Clamp(currHeat + heatIncreaseRate * Time.deltaTime, 0, maxHeat);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);
        if (other.gameObject.tag == "pistol")
        {
            numGuns++;
            other.gameObject.transform.parent = gunHolder.transform;
            other.gameObject.transform.localPosition = new Vector3(Random.Range(-.5f, .5f), .5f, 0);
            other.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90 + Random.Range(-gunDistAngle / 2, gunDistAngle / 2)));
        }
    }
}
