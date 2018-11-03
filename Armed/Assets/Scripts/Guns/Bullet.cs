using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // Speed of the bullet
    float velocity;
    // Lifetime of the bullet before it disappears
    float lifetime;
    // Vector for bullet direction
    Vector2 shotdirection;
    // Bool to keep track of if the bullet is player or enemy spawned, will come in handy later for handling hit detection
    bool shotfromplayer;

	// Use this for initialization
	void Start () {
        // Starts despawn coroutine
        StartCoroutine(DeleteAfterSeconds(lifetime));
        // Checks if the parent is the player, if true then bullet is shot in direction of the mouse
        if (transform.parent.name == "Player")
        {
            shotdirection = Input.mousePosition - transform.position;
            shotfromplayer = true;
        }
        // Other case is when the bullet was shot by an enemy, which in that case the bullet is shot towards the player
        else
        {
            shotdirection = transform.position - GameObject.Find("Player").transform.position;
            shotfromplayer = false;
        }
        // Normalizes the vector so that it doesn't effect the speed of the bullet
        shotdirection.Normalize();
	}
	
	// Update is called once per frame
	void Update () {
        // Moves the bullet
        transform.position += new Vector3(shotdirection.x, shotdirection.y) * velocity * Time.deltaTime;
        // Waiting to handle hit detection until we know more about how spawning and enemies will work
	}

    // Coroutine to delete the bullet after x seconds
    IEnumerator DeleteAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
