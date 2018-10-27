using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol: Gun {

    // Constructor with Base Stats
    public Pistol()
    {

    }
    public Pistol(float coolDownMax, float heatOutput)
    {
        cooldownTime = coolDownMax;
        heatPerShot = heatOutput;
    }

    //public override void MyShoot()
    //{
    //    throw new System.Exception("Not Implemented (yet)");
    //}
}
