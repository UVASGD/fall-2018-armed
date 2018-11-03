using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertPatrolState : AIState {

    float endTime;

    public AlertPatrolState(EnemyMovement enemy)
    {
        currEnemy = enemy;
        endTime = Time.time + enemy.AlertPatrolTime;
    }

    override
    public void MoveBasedOnState()
    {
        print("I'm kinda scared :'(");
        if (Time.time > endTime)
        {
            currEnemy.setState(new PatrolState(currEnemy));
        }
    }
}
