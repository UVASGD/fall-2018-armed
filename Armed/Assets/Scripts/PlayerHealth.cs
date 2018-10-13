using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {


    public float maxhealth, curr_health;
    private float scale;
    private PlayerInventory inventory;
    // Use this for initialization
    void Start () {
        inventory = gameObject.GetComponent<PlayerInventory>();

    }
	
	// Update is called once per frame
	void Update () {
        // Scale the player
        calculateScale();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle Bullets
    }

    public void calculateScale()
    {
        int numItems = inventory.inventoryCount;
        scale = 1;
        transform.localScale = new Vector3(scale, scale, transform.localScale.z);
    }
}
