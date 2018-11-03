using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public List<string> tags;
    
    public Stack<Gun>[] guns = new Stack<Gun>[3]; // Yes, this is an array of stacks of guns, the array is a fixed size though...
    public int[] inventorySlotSizes = new int[4];

    public float throw_speed = 100;

    // Managing max elements in inventory
    public bool inventory_full = false;
    public int max_inventory_items = 25;
    public int formNum;

    public int inventoryCount;
    //Declare prefab objects
    public GameObject objectPrefab;
    public Transform objectSpawn;
    public GameObject pistolPrefab;
    public GameObject shotgunPrefab;
    public GameObject machine_gunPrefab;

    PlayerMovement movement;
    PlayerHealth health;
    // Use this for initialization
    int currentObjectIndex;
    void Start () {
        tags = new List<string> { "object", "pistol", "machine_gun", "shotgun" };
        print(tags.ToString());
        currentObjectIndex = 0;
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        ThrowyBoi();
        ShootyBoi();
    }

    //Throws Things
    void ThrowyBoi()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && inventoryCount > 0)
        {
            GameObject projectile;
            //var inst = new GameObject();
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
            projectile = Instantiate(Resources.Load(tags[currentObjectIndex], typeof(GameObject)), objectSpawn.position, objectSpawn.rotation) as GameObject;
            if(currentObjectIndex > 0)
            {
                guns[currentObjectIndex - 1].Pop();
            }
            projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.up * throw_speed;   //Add momentum?
            //inventory.RemoveAt(a - 1);
            inventoryCount--;
            if (inventoryCount == 4 || inventoryCount == 9 || inventoryCount == 14 || inventoryCount == 19)
            {
                formNum--;
                health.calculateScale(formNum);
                movement.calculateMovementSpeed(formNum);
                // call health and movement
            }
            inventory_full = false;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (inventoryCount > 0)
            {
                currentObjectIndex = (currentObjectIndex + 1) % 4;
                while(inventorySlotSizes[currentObjectIndex] <= 0)
                {
                    currentObjectIndex = (currentObjectIndex + 1) % 4;
                }
            }
            
        }
    }

    // Makes Da Guns go Boom
    //#TODO - Heat-based system Mech-Warrior style for the guns
    void ShootyBoi()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            for (int i = 0; i < 3; i++)
            {
                foreach (Gun g in guns[i])
                {
                    g.Shoot();
                    // Replace with currentHeat += g.Shoot(); or whatever when heat is set up
                }
            }
        }
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
        if (tags.Contains(collision.gameObject.tag) && Input.GetKey(KeyCode.Mouse1) && !inventory_full)
        {
            if (collision.gameObject.tag.Equals("object")){
                inventorySlotSizes[0]++;
            }
            else
            {
                switch(collision.gameObject.tag)
                {
                    case "pistol":
                        guns[0].Push(new Pistol());
                        inventorySlotSizes[1]++;
                        break;
                    case "machine_gun":
                        guns[1].Push(new MachineGun());
                        inventorySlotSizes[2]++;
                        break;
                    case "shotgun":
                        guns[2].Push(new Shotgun());
                        inventorySlotSizes[3]++;
                        break;
                }

            }
            inventoryCount++;
            if (inventoryCount == 5 || inventoryCount == 10 || inventoryCount == 15 || inventoryCount == 20)
            {
                formNum++;
                health.calculateScale(formNum);
                movement.calculateMovementSpeed(formNum);
            }
            if (inventoryCount == max_inventory_items)
            {
                inventory_full = true;
            }
            Destroy(collision.gameObject);
        }
        ////A debugging step to see if items are being added to the inventory correctly
        //foreach (string str in inventory)
        //{
        //    print(str);
        //}
    }
}
