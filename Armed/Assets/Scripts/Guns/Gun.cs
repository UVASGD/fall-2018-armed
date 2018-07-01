using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Gun : MonoBehaviour {

    float cooldownTime;
    float currentCooldown;
    float heatPerShot;

    public void Shoot()
    {
        if(currentCooldown >= cooldownTime)
        {
            MyShoot();
        }
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
