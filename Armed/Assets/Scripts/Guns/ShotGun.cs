using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun {

    // Constructor with Base Stats
    public Shotgun()
    {

    }
    public Shotgun(float coolDownMax, float heatOutput)
    {
        cooldownTime = coolDownMax;
        heatPerShot = heatOutput;
    }

    public override void MyShoot()
    {
        throw new System.Exception("Not Implemented (yet)");
    }
}
