using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public GameObject NavmeshHolder;
    public Navpoint start;
    public Navpoint end;

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

        start = transforms[minIndex].GetComponent<Navpoint>();
        end = transforms[Random.Range(0, numNavpoints)].GetComponent<Navpoint>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
