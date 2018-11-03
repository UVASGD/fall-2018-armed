using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroState : AIState {

    float endTime;

    public AggroState(EnemyMovement enemy)
    {
        currEnemy = enemy;
        endTime = Time.time + enemy.AggroTime;
    }

	override
    public void MoveBasedOnState()
    {
        print("IM ANGRY :(");
        if (Time.time > endTime)
        {
            currEnemy.setState(new AlertPatrolState(currEnemy));
        }
    }

}
