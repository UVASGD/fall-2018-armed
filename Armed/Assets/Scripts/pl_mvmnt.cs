using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_mvmnt : MonoBehaviour {

    public float speed; //Declare Speed
    public float maxstamina,curr_stamina,stamina_regen;  //Declare maxstamina, current stamina, and stamina_regen

    // Use this for initialization
    void Start () {
        curr_stamina = maxstamina;
    }

    //Function that checks for sprinting, returns speed, and lowers stamina if sprintin
    void Sprinting(ref float speed, ref float curr_stamina, ref float stamina_regen)
        {
        if (Input.GetKey(KeyCode.LeftShift) && curr_stamina > 0) {
            speed = 0.25f;
            curr_stamina --;
            stamina_regen = 0;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 0.15f;
            stamina_regen = 1;
        }
        else
        {
            speed = 0.15f;
        }
    }

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
    
    //Function that finds the angle between two specified vector points
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    
}
//Scrap
