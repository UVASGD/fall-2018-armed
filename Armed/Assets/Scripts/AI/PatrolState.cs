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
            currEnemy.targetPoint = currEnemy.patrolPoints[currEnemy.currPoint++ % currEnemy.patrolPoints.Count];
            Vector3 rand = Random.insideUnitCircle;
            currEnemy.targetMove = currEnemy.targetPoint.transform.position + rand;
            float angle = Vector2.SignedAngle(Vector2.up, currEnemy.targetMove - currEnemy.transform.position);
            currEnemy.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));  //Rotate Player
        }

        // The step size is equal to speed times frame time.
        float step = currEnemy.speed * Time.deltaTime;

        // Move our position a step closer to the target.
        currEnemy.transform.position = Vector3.MoveTowards(currEnemy.transform.position, currEnemy.targetMove, step);
    }

    public override void StateExit()
    {
        throw new System.NotImplementedException();
    }

    public override void StateEnter()
    {
        List<Navpoint> nextPoint = currEnemy.patrolPoints;
        double minDist = Mathf.Infinity;
        int minIndex = -1;
        for (int x = 0; x < nextPoint.Count; x++)
        {
            double currDist = Vector3.SqrMagnitude(currEnemy.transform.position - nextPoint[x].transform.position);
            if (currDist < minDist)
            {
                minDist = currDist;
                minIndex = x;
            }
        }
        currEnemy.targetPoint = nextPoint[minIndex];
        currEnemy.targetMove = currEnemy.targetPoint.transform.position;
        currEnemy.currPoint = minIndex;
        float angle = Vector2.SignedAngle(Vector2.up, currEnemy.targetMove - currEnemy.transform.position);
        currEnemy.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));  //Rotate Player
    }
}
