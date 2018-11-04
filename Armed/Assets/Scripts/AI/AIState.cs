using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AIState : MonoBehaviour {

    protected EnemyMovement currEnemy;

    abstract public void MoveBasedOnState();
    abstract public void StateExit();
    abstract public void StateEnter();
}
