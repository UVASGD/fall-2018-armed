using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_mvmnt : MonoBehaviour {

    public float speed; //Declare Speed
    public float maxstamina,curr_stamina,stamina_regen;  //Declare maxstamina, current stamina, and stamina_regen
    //public int inventory;   //Declare inventory, the number of objects currently held by the player
    private List<string> inventory = new List<string>();    //Declare inventory list, which will store the objects
    private Rigidbody2D rb2d;

    //Declare prefab objects
    public GameObject obj = (GameObject) Instantiate(Resources.Load("object"));
    public GameObject box = (GameObject)Instantiate(Resources.Load("box"));
    public GameObject sq = (GameObject)Instantiate(Resources.Load("square"));
    public GameObject flat = (GameObject)Instantiate(Resources.Load("flat cube"));

    // Use this for initialization
    void Start () {
        curr_stamina = maxstamina;
        rb2d = GetComponent<Rigidbody2D> (); //Get Rigidbody
        

    }

    //Function that checks for sprinting, returns speed, and lowers stamina if sprintin
    void Sprinting(ref float speed, ref float curr_stamina, ref float stamina_regen)
        {
        if (Input.GetKey(KeyCode.LeftShift) && curr_stamina > 0) {
            speed = 15f;
            curr_stamina --;
            stamina_regen = 0;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 8f;
            stamina_regen = 1;
        }
        else
        {
            speed = 8f;
        }
    }

    //FixedUpdate is independant of frame rate <3
    private void FixedUpdate()
    {        
        //Updates speed and curr_stamina by checking for sprinting
        Sprinting(ref speed, ref curr_stamina, ref stamina_regen);

        //Update Stamina
        if (curr_stamina < 0)
        {
            curr_stamina = 0;
        }
        else if (curr_stamina > maxstamina-1)
        {
            curr_stamina = maxstamina;
        }
        else
        {
            curr_stamina = curr_stamina + stamina_regen;
        }

        //Movement of the Player
        float HorizontalMove = Input.GetAxis("Horizontal");
        float VerticalMove = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(HorizontalMove*speed, VerticalMove*speed);
        transform.Translate(move * Time.deltaTime,Space.World);

        //Rotation of the Player
        Vector2 playerpos = Camera.main.WorldToViewportPoint(transform.position);   //Define Player Position
        Vector2 mousepos = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition); //Define Mouse Position
        float angle = AngleBetweenTwoPoints(playerpos, mousepos) + 90;   //Solve for the angle between the player and mouse
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));  //Rotate Player

        //Check for Throwing item
        if (Input.GetKeyDown(KeyCode.Mouse0) && inventory.Count > 0)
        {
            var inst = new GameObject();
            int a = inventory.Count;
            print("The inventory has stuff in it");
            print("The length of the inventory is " + a);
            //if (inventory[a - 1] == "object")
            //{
            //    inst = obj;
            //}
            //else if (inventory[a - 1] == "box")
            //{
            //    inst = box;
            //}
            //else if (inventory[a - 1] == "square")
            //{
            //    inst = sq;
            //}
            //else if (inventory[a-1] == "flat cube")
            //{
            //    inst = flat;
            //}

            //Instantiate(inst, new Vector3(0, 0, 0), Quaternion.identity);
            
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Detect if player is pulling an object towards them
        if (collision.gameObject.tag == "object" && Input.GetKey(KeyCode.Mouse1))
        {
            //Debug.Log("It can see it!"); //Figure out how to pull towards the player plz <3    -Past me
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Detect if player collects an object
        if(collision.gameObject.tag == "object" && Input.GetKey(KeyCode.Mouse1))
        {
            //Debug.Log("It hit the player!");    //Figure out how to make object part of player plz <3   -Slightly less past me
            inventory.Add(collision.gameObject.name);
            Destroy(collision.gameObject);
            //foreach (string str in inventory)
            //{
            //    print(str);
            //}

        }
    }


    //Function that finds the angle between two specified vector points
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
  
}


//Scrap
/*
   // Update is called once per frame
   void Update () {
       //Updates speed and curr_stamina by checking for sprinting
       Sprinting(ref speed,ref curr_stamina, ref stamina_regen);   

       //Update Sprinting
       if (curr_stamina < 0)
       {
           curr_stamina = 0;
       }
       else if (curr_stamina > maxstamina)
       {
           curr_stamina = maxstamina;
       }
       else
       {
           curr_stamina = curr_stamina + stamina_regen;
       }

       //Movement of the Player
       if (Input.GetKey(KeyCode.D))
       {
           Vector3 position = this.transform.position;
           position.x=position.x+speed;
           this.transform.position = position;
       }
       if (Input.GetKey(KeyCode.A))
       {
           Vector3 position = this.transform.position;
           position.x=position.x-speed;
           this.transform.position = position;
       }
       if (Input.GetKey(KeyCode.W))
       {
           Vector3 position = this.transform.position;
           position.y=position.y+speed;
           this.transform.position = position;
       }
       if (Input.GetKey(KeyCode.S))
       {
           Vector3 position = this.transform.position;
           position.y=position.y-speed;
           this.transform.position = position;
       }

       //Rotation of the Player
       Vector2 playerpos = Camera.main.WorldToViewportPoint(transform.position);   //Define Player Position
       Vector2 mousepos = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition); //Define Mouse Position
       float angle = AngleBetweenTwoPoints(playerpos, mousepos)+90;   //Solve for the angle between the player and mouse
       transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));  //Rotate Player
   }
   */

/*
 * //Movement of the Player
if (Input.GetKey(KeyCode.D))
{
    Vector3 position = this.transform.position;
    position.x = position.x + speed;
    this.transform.position = position;
}
if (Input.GetKey(KeyCode.A))
{
    Vector3 position = this.transform.position;
    position.x = position.x - speed;
    this.transform.position = position;
}
if (Input.GetKey(KeyCode.W))
{
    Vector3 position = this.transform.position;
    position.y = position.y + speed;
    this.transform.position = position;
}
if (Input.GetKey(KeyCode.S))
{
    Vector3 position = this.transform.position;
    position.y = position.y - speed;
    this.transform.position = position;
}
*/

/*
   //Movement with Rigidbodies and Forces
   //Update speed and curr_stamina by checking for sprinting
   Sprinting(ref speed, ref curr_stamina, ref stamina_regen);

   //Update Stamina
   if (curr_stamina < 0)
   {
       curr_stamina = 0;
   }
   else if (curr_stamina > maxstamina)
   {
       curr_stamina = maxstamina;
   }
   else
   {
       curr_stamina = curr_stamina + stamina_regen;
   }

   //Get Horizontal Movement
   float HorizontalMove = Input.GetAxis("Horizontal");

   //Get Vertical Movement
   float VerticalMove = Input.GetAxis("Vertical");

   //Create movement Vector
   Vector2 move = new Vector2(HorizontalMove, VerticalMove);
   //Debug.Log(move);
   //AddForce to rigidbody to move player (speed already defined)
   rb2d.AddForce(move * speed);

   //Rotation of the Player
   Vector2 playerpos = Camera.main.WorldToViewportPoint(transform.position);   //Define Player Position
   Vector2 mousepos = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition); //Define Mouse Position
   float angle = AngleBetweenTwoPoints(playerpos, mousepos) + 90;   //Solve for the angle between the player and mouse
   transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));  //Rotate Player

   //Friction: When the player isn't moving, they need to stop moving :(
   Debug.Log(Input.GetAxis("Horizontal"));
   */
