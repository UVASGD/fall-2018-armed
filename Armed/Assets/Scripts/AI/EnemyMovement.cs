using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public GameObject player;
    public GameObject NavmeshHolder;

    public List<Navpoint> patrolPoints;
    public Navpoint targetPoint;
    public Vector3 targetMove;
    public int currPoint;

    public AIState currState;
    public float AggroTime = 10;
    public float AlertPatrolTime = 10;
    public float coneOfVisionAngle = 30;
    public float coneOfVisionDist = 100;
    public float speed = 2;


    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        NavmeshHolder = GameObject.Find("Navmesh");
        if (patrolPoints.Count == 0)
        {
            Navpoint[] availablePoints = NavmeshHolder.GetComponentsInChildren<Navpoint>();
            SortedList toChooseFrom = new SortedList();
            for (int x = 0; x < availablePoints.Length; x++)
            {
                toChooseFrom.Add(-Vector3.SqrMagnitude(availablePoints[x].transform.position - transform.position), availablePoints[x]);
            }
            patrolPoints.Add((Navpoint)toChooseFrom.GetByIndex(0));
            for (int x = 0; x < 2; x++)
            {
                int randInt = Random.Range(0, patrolPoints[x].nearest.Count);
                patrolPoints.Add(patrolPoints[x].nearest[randInt]);
            }
            for (int x = 0; x < 2; x++)
            {
                patrolPoints.Add(patrolPoints[2 - x - 1]);
            }
        }
        setState(new PatrolState(this));
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
        if (currState != null)
            currState.StateExit();
        currState = newState;
        currState.StateEnter();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "object")
        {
            setState(new AggroState(this));
        }
    }
}
