using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AIState : MonoBehaviour {


	
    abstract public void MoveBasedOnState(Vector3 targetMove, float speed, Navpoint targetPoint);

}
