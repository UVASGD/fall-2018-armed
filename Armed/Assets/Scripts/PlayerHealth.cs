using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public readonly int BULLET_LAYER = 13;
    public int bulletDamage;
    public float maxhealth, curr_health;
    float[] healthTiers = { 60, 80, 100, 120, 140 };
    float health_percent;
    private float scale;
    // Scaler should be under 1 for scaling scripts to work properly
    private float scaler = .85f;
    private GunManager gunManager;
    // Use this for initialization
    void Start () {
        maxhealth = healthTiers[0];
        curr_health = healthTiers[0];
        gunManager = GetComponent<GunManager>();
        scale = gameObject.transform.localScale.x;
    }
	
	// Update is called once per frame
	void Update () {
        // Scale the player
        // change to only happen when inventory calls it
        //calculateScale();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.tag);
        if (collision.gameObject.layer == BULLET_LAYER)
        {
            print("BULLET HIT ME");
            TakeBulletDamage();
        }
        if (collision.gameObject.tag == "healthpack")
        {
            print("Picked up health");
            Destroy(collision.gameObject);
            UseHealthPack();
        }
    }
    private void TakeBulletDamage()
    {
        if (curr_health - bulletDamage < 0)
        {
            curr_health = 0;
            foreach (Transform child in gameObject.transform)
            {
                child.transform.parent = null;
            }
            Destroy(gameObject);
        }
        else
        {
            curr_health -= bulletDamage;
        }
        if(curr_health <= 0)
        {
            StartCoroutine("level_change");
        }
    }
    private void UseHealthPack()
    {
        curr_health += 0.5f*maxhealth;
    }

    public void calculateHealth(int form)
    {
        // Changes health based on amount of items in inventory
        // todo: movement speed inverse to inventory, player size increases as inventory increases
        // Changes health and max health based on the number of items in your inventory
        // At breakpoints of 3 and 4 items, will change maxhealth and then scale current health based on the percent of maxhealth you previously had

        health_percent = curr_health / maxhealth;
        maxhealth = healthTiers[form];
        curr_health = maxhealth * health_percent;
        gunManager.shotsPerVolley = Mathf.Max(1, form);
    }

    public void scaleDown()
    {
        scale *= scaler;
        transform.localScale = new Vector3(scale, scale, transform.localScale.z);
    }

    public void scaleUp()
    {
        scale /= scaler;
        transform.localScale = new Vector3(scale, scale, transform.localScale.z);
    }

    IEnumerator level_change()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(4); //Load Level
        //Wait until fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
