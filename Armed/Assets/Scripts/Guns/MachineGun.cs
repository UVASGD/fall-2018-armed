using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Gun {

    // Constructor with Base Stats
    public MachineGun()
    {

    }
    public MachineGun(float coolDownMax, float heatOutput)
    {
        cooldownTime = coolDownMax;
        heatPerShot = heatOutput;
    }

    public override void MyShoot()
    {
        throw new System.Exception("Not Implemented (yet)");
    }
}
