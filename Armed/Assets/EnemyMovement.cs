using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public GameObject NavmeshHolder;
    public float speed;
    public Navpoint targetPoint;
    public Vector3 targetMove;
    public AIState currState;

	// Use this for initialization
	void Start () {
        int numNavpoints = NavmeshHolder.transform.childCount;
        Transform[] transforms = NavmeshHolder.GetComponentsInChildren<Transform>();
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

        targetPoint = transforms[minIndex].GetComponent<Navpoint>();
        targetMove = targetPoint.transform.position;
        currState = new PatrolState();
	}
	
	// Update is called once per frame
	void Update () {
        currState.MoveBasedOnState(targetMove, speed, targetPoint);
    }  
}
