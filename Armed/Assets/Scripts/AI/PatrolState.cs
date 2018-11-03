using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : AIState {

    override
    public void MoveBasedOnState(Vector3 targetMove, float speed, Navpoint targetPoint)
    {
        if (Vector3.Distance(transform.position, targetMove) < .1)
        {
            Navpoint[] nextPoint = targetPoint.GetComponent<Navpoint>().nearest;
            targetPoint = nextPoint[Random.Range(0, nextPoint.Length)];
            targetMove = targetPoint.transform.position + Random.insideUnitSphere;
        }

        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, targetMove, step);
    }
}
