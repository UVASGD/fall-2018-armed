using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float cooldownTime;
    public float currentCooldown;
    protected float heatPerShot;

    public Transform bullet;

    // Returns the heat of the gun for the inventory to add
    public float Shoot()
    {
        if(currentCooldown >= cooldownTime)
        {
            currentCooldown = 0;
            InstantiateBullet();
            return heatPerShot;
        }
            return 0;
    }

    public void IncrementCooldown(float increment)
    {
        if (increment <= 0)
        {
            throw new System.Exception("Cooldown Increment must be greater than zero.");
        }
        currentCooldown += increment;
        if (currentCooldown > cooldownTime)
        {
            currentCooldown = cooldownTime;
        }
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
