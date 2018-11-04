using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToPatrolState : AIState
{

    public BackToPatrolState(EnemyMovement enemy)
    {
        currEnemy = enemy;
    }

    public override void MoveBasedOnState()
    {
        if (Vector3.Distance(currEnemy.transform.position, currEnemy.targetMove) < .1)
        {
            List<Navpoint> nextPoint = currEnemy.targetPoint.nearest;
            SortedList toChooseFrom = new SortedList();
            for (int x = 0; x < nextPoint.Count; x++)
            {
                toChooseFrom.Add(-Vector3.SqrMagnitude(nextPoint[x].transform.position - currEnemy.patrolPoints[0].transform.position), nextPoint[x]);
            }
            currEnemy.targetPoint = (Navpoint)toChooseFrom.GetByIndex(0);
            Vector3 rand = Random.insideUnitCircle;
            currEnemy.targetMove = currEnemy.targetPoint.transform.position + rand;

            if (currEnemy.patrolPoints.Contains(currEnemy.targetPoint))
            {
                currEnemy.currPoint = currEnemy.patrolPoints.IndexOf(currEnemy.targetPoint);
                currEnemy.setState(new PatrolState(currEnemy));
            }
            float angle = Vector2.SignedAngle(Vector2.up, currEnemy.targetMove - currEnemy.transform.position);
            currEnemy.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));  //Rotate Player
        }

        // The step size is equal to speed times frame time.
        float step = currEnemy.speed * Time.deltaTime;

        // Move our position a step closer to the target.
        currEnemy.transform.position = Vector3.MoveTowards(currEnemy.transform.position, currEnemy.targetMove, step);
    }

    public override void StateEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void StateExit()
    {
        throw new System.NotImplementedException();
    }
}
