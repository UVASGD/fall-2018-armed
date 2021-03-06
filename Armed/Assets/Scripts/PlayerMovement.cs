﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Variables which movement is directly affected by and which
    // will be constantly updated by the next set of variables
    private float speed;
    private float curr_stamina, stamina_regen;
    private bool stamina_wait = false;

    // Normal and sprinting movement speeds, will be updated using speedX variables
    public float base_speed, sprint_speed;
    // Sprinting variables used to adjust when and how often you can sprint
    public float sprint_regen, slow_regen, base_regen;
    public float maxstamina;
    public bool useMouse = true;

    // Speeds for all the different speedtiers
    float speedtier1 = 10.3f;
    float speedtier2 = 8f;
    float speedtier3 = 5.6f;
    float speedtier4 = 3.92f;
    float speedtier5 = 2.75f;
    // Percent increase in speed when sprinting
    float sprintdelta = 1.5f;

    private Rigidbody2D rb2d;

    // Removed inventory access since not used

    // Initialize variables
    void Start()
    {
        curr_stamina = maxstamina;
        rb2d = GetComponent<Rigidbody2D>(); //Get Rigidbody
    }

    //Check for sprinting, returns speed, and lowers stamina if sprinting
    void Sprinting(ref float speed, ref float curr_stamina, ref float stamina_regen)
    {
        // Sprints if sprint key held and sufficient stamina
        if (Input.GetKey(KeyCode.LeftShift) && stamina_wait == false)
        {
            stamina_regen = sprint_regen;
            speed = sprint_speed;
        }
        // Tried to sprint but not enough stamina
        else if (stamina_wait == true)
        {
            speed = base_speed;
            stamina_regen = slow_regen;
        }
        // Not sprinting and stamina regen is normal
        else
        {
            speed = base_speed;
            stamina_regen = base_regen;
        }

        curr_stamina = Mathf.Clamp(curr_stamina + (stamina_regen * Time.deltaTime), 0.0f, maxstamina);

        if (curr_stamina == 0)
        {
            stamina_wait = true;
        }
        else if (curr_stamina == maxstamina)
        {
            stamina_wait = false;
        }

        //Debug.Log(curr_stamina);
        //Debug.Log(stamina_wait);
    }

    //Update player movement, throw items, etc. 
    private void FixedUpdate()
    {
        //Updates speed and curr_stamina by checking for sprinting
        Sprinting(ref speed, ref curr_stamina, ref stamina_regen);

        //Movement of the Player
        float HorizontalMove = Input.GetAxis("Horizontal");
        float VerticalMove = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(HorizontalMove * speed, VerticalMove * speed);
        transform.Translate(move * Time.deltaTime, Space.World);

        //Rotation of the Player
        float angle;
        if (useMouse)
        {
            Vector2 playerpos = Camera.main.WorldToViewportPoint(transform.position);   //Define Player Position
            Vector2 mousepos = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition); //Define Mouse Position
            angle = Vector2.SignedAngle(Vector2.up, mousepos - playerpos);   //Solve for the angle between the player and mouse
        }
        else
        {
            Vector2 analogStickPos = new Vector2(Input.GetAxis("Right Joystick X"), Input.GetAxis("Right Joystick Y"));
            angle = Vector2.SignedAngle(Vector2.up, analogStickPos);
        }
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));  //Rotate Player

        //Check for Throwing item


    }

    public void calculateMovementSpeed(int form)
    {
        // Calculates base and sprinting speeds based on the form number
        switch(form)
        {
            case 0:
                base_speed = speedtier1;
                break;
            case 1:
                base_speed = speedtier1;
                break;
            case 2:
                base_speed = speedtier2;
                break;
            case 3:
                base_speed = speedtier3;
                break;
            case 4:
                base_speed = speedtier4;
                break;
            case 5:
                base_speed = speedtier5;
                break;
            default:
                break;
        }
        // Sprint speed is set as at the new base speed times the sprinting modifier
        // which is a constant percent increase
        sprint_speed = base_speed * sprintdelta;
    }

	public float getCurStamina(){ return curr_stamina;}
	public float getMaxStamina() { return maxstamina; }
}