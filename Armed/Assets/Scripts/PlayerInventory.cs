using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public List<string> inventory;
    public List<string> tags;

    public float throw_speed = 100;

    //Declare prefab objects
    public GameObject objectPrefab;
    public Transform objectSpawn;
    public GameObject pistolPrefab;
    public GameObject shotgunPrefab;
    public GameObject machine_gunPrefab;
    // Use this for initialization
    void Start () {
        inventory = new List<string>();
        tags = new List<string> { "object", "pistol", "machine_gun", "shotgun" };
        print(tags.ToString());
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        ThrowyBoi();
        ScaleyBoi();
        ShootyBoi();
    }

    //Throws Things
    void ThrowyBoi()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && inventory.Count > 0)
        {
            GameObject projectile;
            //var inst = new GameObject();
            int a = inventory.Count;
            print("The inventory has stuff in it");
            print("The length of the inventory is " + a);
            print(inventory[a - 1]);
            //There's got to be a better way of doing this... Jared, halp plz
            //if (inventory[a - 1] == "object")
            //{
            //    projectile = Instantiate(objectPrefab, objectSpawn.position, objectSpawn.rotation);
            //}
            //else if (inventory[a - 1] == "pistol")
            //{
            //    projectile = Instantiate(pistolPrefab, objectSpawn.position, objectSpawn.rotation);
            //}
            //else if (inventory[a - 1] == "shotgun")
            //{
            //    projectile = Instantiate(shotgunPrefab, objectSpawn.position, objectSpawn.rotation);
            //}
            //else if (inventory[a - 1] == "machine_gun")
            //{
            //    projectile = Instantiate(machine_gunPrefab, objectSpawn.position, objectSpawn.rotation);
            //}
            // https://answers.unity.com/questions/551934/instantiating-using-a-string-for-prefab-name.html
            projectile = Instantiate(Resources.Load(inventory[a - 1], typeof(GameObject)), objectSpawn.position, objectSpawn.rotation) as GameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.up * throw_speed;   //Add momentum?
            inventory.Remove(inventory[a - 1]);


        }
    }

    // Makes Da Guns go Boom
    //#TODO - Heat-based system Mech-Warrior style for the guns
    void ShootyBoi()
    {
        // #TODO
    }

    // Scales the player based on the inventory
    void ScaleyBoi()
    {
        // 4 tiers
        // Add 50 health
        //#TODO
        // Also different sized sprites
    }


    //Detecting if players try to collect objects
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Detect if player is pulling an object towards them
        if (tags.Contains(collision.gameObject.tag) && Input.GetKey(KeyCode.Mouse1))
        {
            Debug.Log("It can see it!"); //Figure out how to pull towards the player plz <3    -Past me
        }
    }

    //Detecting if players pick up items
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Detect if player collects an object
        if (tags.Contains(collision.gameObject.tag) && Input.GetKey(KeyCode.Mouse1))
        {
            inventory.Add(collision.gameObject.tag);
            Destroy(collision.gameObject);
        }
        ////A debugging step to see if items are being added to the inventory correctly
        //foreach (string str in inventory)
        //{
        //    print(str);
        //}
    }
}
