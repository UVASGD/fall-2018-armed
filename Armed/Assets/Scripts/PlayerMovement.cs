﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Declare needed variables
    private float speed;
    private float curr_stamina, stamina_regen;
    private bool stamina_wait = false;

    public float base_speed, sprint_speed;
    public float sprint_regen, slow_regen, base_regen;
    public float maxstamina;

    public float speedtier1, speedtier2, speedtier3, speedtier4, speedtier5;
    public float speedtierdelta = .7f;

    private Rigidbody2D rb2d;

    private PlayerInventory inventory;

    // Initialize variables
    void Start()
    {
        curr_stamina = maxstamina;
        rb2d = GetComponent<Rigidbody2D>(); //Get Rigidbody
        speedtier1 = 10.3f;
        speedtier2 = speedtier1 * speedtierdelta;
        speedtier3 = speedtier2 * speedtierdelta;
        speedtier4 = speedtier3 * speedtierdelta;
        speedtier5 = speedtier4 * speedtierdelta;
    }

    //Check for sprinting, returns speed, and lowers stamina if sprinting
    void Sprinting(ref float speed, ref float curr_stamina, ref float stamina_regen)
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamina_wait == false)
        {
            stamina_regen = sprint_regen;
            speed = sprint_speed;
        }
        else if (stamina_wait == true)
        {
            speed = base_speed;
            stamina_regen = slow_regen;
        }
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
        Vector2 playerpos = Camera.main.WorldToViewportPoint(transform.position);   //Define Player Position
        Vector2 mousepos = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition); //Define Mouse Position
        float angle = Vector2.SignedAngle(Vector2.up, mousepos - playerpos);   //Solve for the angle between the player and mouse
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));  //Rotate Player

        //Check for Throwing item


    }

    public void calculateMovementSpeed(int form)
    {
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
    }
}