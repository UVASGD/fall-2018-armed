using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float cooldownTime = 30f;
    public float currentCooldown = 0;
    protected float heatPerShot;
    public int numBullets = 30;

    public Transform bullet;

    void Update()
    {
        this.currentCooldown += Time.deltaTime;
    }

        // Returns the heat of the gun for the inventory to add
    public bool Shoot()
    {
        if(currentCooldown >= cooldownTime)
        {
            currentCooldown = 0;
            InstantiateBullet();
            return true;
        }
        return false;
    }

    public void InstantiateBullet() {
        Vector3 offset = this.transform.right;
        Object.Instantiate(
            bullet,
            new Vector3(
                this.transform.position.x + this.transform.right.x,
                this.transform.position.y + this.transform.right.y,
                this.transform.position.z + this.transform.right.z),
            this.transform.rotation
        );
    }
}
