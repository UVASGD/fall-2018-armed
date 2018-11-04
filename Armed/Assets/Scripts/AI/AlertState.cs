using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : AIState {

    float endTime;

    public AlertState(EnemyMovement enemy)
    {
        currEnemy = enemy;
        endTime = Time.time + enemy.AlertPatrolTime;
    }

    override
    public void MoveBasedOnState()
    {
        print("I'm kinda scared :'(");
        if (Vector3.Distance(currEnemy.transform.position, currEnemy.targetMove) < .1)
        {
            List<Navpoint> nextPoint = currEnemy.targetPoint.nearest;
            currEnemy.targetPoint = nextPoint[Random.Range(0, nextPoint.Count)];
            Vector3 rand = Random.insideUnitCircle;
            currEnemy.targetMove = currEnemy.targetPoint.transform.position + rand;
            float angle = Vector2.SignedAngle(Vector2.up, currEnemy.targetMove - currEnemy.transform.position);
            currEnemy.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));  //Rotate Player
        }

        // The step size is equal to speed times frame time.
        float step = currEnemy.speed * Time.deltaTime;

        // Move our position a step closer to the target.
        currEnemy.transform.position = Vector3.MoveTowards(currEnemy.transform.position, currEnemy.targetMove, step);

        if (Time.time > endTime)
        {
            currEnemy.setState(new BackToPatrolState(currEnemy));
        }
    }

    public override void StateEnter()
    {
        int numNavpoints = currEnemy.NavmeshHolder.transform.childCount;
        Transform[] transforms = currEnemy.NavmeshHolder.GetComponentsInChildren<Transform>();
        double minDist = Mathf.Infinity;
        int minIndex = -1;
        for (int x = 0; x < numNavpoints; x++)
        {
            double currDist = Vector3.Distance(transform.position, transforms[x].position);
            if (currDist < minDist)
            {
                minDist = currDist;
                minIndex = x;
            }
        }
        currEnemy.targetPoint = transforms[minIndex].GetComponent<Navpoint>();
        currEnemy.targetMove = currEnemy.targetPoint.transform.position;
    }

    public override void StateExit()
    {
        throw new System.NotImplementedException();
    }
}
