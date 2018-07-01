using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Gun {

    public float cooldownTime;
    public float currentCooldown;
    float heatPerShot;

    // Returns the heat of the gun for the inventory to add
    public float Shoot()
    {
        if(currentCooldown >= cooldownTime)
        {
            MyShoot();
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
    abstract public void MyShoot(); // How each kind of gun shoots
}
