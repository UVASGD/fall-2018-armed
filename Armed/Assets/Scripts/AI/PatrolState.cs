using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : AIState {

    public PatrolState(EnemyMovement enemy)
    {
        currEnemy = enemy;
    }

    override
    public void MoveBasedOnState()
    {
        print("I'm pretty happy :)");
        if (Vector3.Distance(currEnemy.transform.position, currEnemy.targetMove) < .1)
        {
            Navpoint[] nextPoint = currEnemy.targetPoint.nearest;
            currEnemy.targetPoint = nextPoint[Random.Range(0, nextPoint.Length)];
            Vector3 rand = Random.insideUnitCircle;
            currEnemy.targetMove = currEnemy.targetPoint.transform.position + rand;
        }

        // The step size is equal to speed times frame time.
        float step = currEnemy.speed * Time.deltaTime;

        // Move our position a step closer to the target.
        currEnemy.transform.position = Vector3.MoveTowards(currEnemy.transform.position, currEnemy.targetMove, step);
    }
}
