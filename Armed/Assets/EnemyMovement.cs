using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public GameObject NavmeshHolder;
    public float speed;
    public Navpoint targetPoint;
    public Vector3 targetMove;
    public AIState currState;
    public GameObject player;
    public float coneOfVisionAngle = 30;
    public float coneOfVisionDist = 100;

    public float AggroTime = 10;
    public float AlertPatrolTime = 10;

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
        currState = new PatrolState(this);
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (playerInVision())
        {
            currState = new AggroState(this);
        }
        currState.MoveBasedOnState();
    }

    bool playerInVision()
    {
        
        Vector3 targetDirection = player.transform.position - transform.position;
        if (Vector3.Angle(targetDirection, transform.up) < coneOfVisionAngle)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection);
            if (hit.collider != null && hit.collider.name == "Player")
            {
                return true;
            }
        }
        return false;
    }

    public void setState(AIState newState)
    {
        currState = newState;
    }
}
