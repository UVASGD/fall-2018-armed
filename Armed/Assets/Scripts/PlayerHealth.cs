using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float maxhealth, curr_health;
    public float tier1health, tier2health, tier3health, tier4health, tier5health;
    public float health_percent;
    private float scale;
    private PlayerInventory inventory;
    // Use this for initialization
    void Start () {
        inventory = gameObject.GetComponent<PlayerInventory>();
    }
	
	// Update is called once per frame
	void Update () {
        // Scale the player
        // change to only happen when inventory calls it
        //calculateScale();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle Bullets
    }

    public void calculateHealth(int form)
    {
        // Changes health based on amount of items in inventory
        // todo: movement speed inverse to inventory, player size increases as inventory increases
        switch(form)
        {
            // Changes health and max health based on the number of items in your inventory
            // At breakpoints of 3 and 4 items, will change maxhealth and then scale current health based on the percent of maxhealth you previously had
            case 0:
                maxhealth = tier1health;
                break;
            case 1:
                health_percent = curr_health / maxhealth;
                maxhealth = tier1health;
                curr_health = maxhealth * health_percent;
                break;
            case 2:
                health_percent = curr_health / maxhealth;
                maxhealth = tier2health;
                curr_health = maxhealth * health_percent;
                break;
            case 3:
                health_percent = curr_health / maxhealth;
                maxhealth = tier3health;
                curr_health = maxhealth * health_percent;
                break;
            case 4:
                health_percent = curr_health / maxhealth;
                maxhealth = tier4health;
                curr_health = maxhealth * health_percent;
                break;
            case 5:
                health_percent = curr_health / maxhealth;
                maxhealth = tier5health;
                curr_health = maxhealth * health_percent;
                break;
            default:
                break;
        }
        scale = 1;
        transform.localScale = new Vector3(scale, scale, transform.localScale.z);
    }
}
