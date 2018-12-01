using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // Speed of the bullet
    float velocity = 15f;
    // Lifetime of the bullet before it disappears
    float lifetime = 3f;

	// Use this for initialization
	void Start () {
        // Starts despawn coroutine
        StartCoroutine(DeleteAfterSeconds(lifetime));
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += this.transform.right * velocity * Time.deltaTime;
	}

    // Coroutine to delete the bullet after x seconds
    IEnumerator DeleteAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
