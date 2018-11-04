using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navpoint : MonoBehaviour {

    public List<Navpoint> nearest;

    void Awake()
    {
        // Autopopulate the nearest list

        Transform[] allNavpoints = transform.parent.GetComponentsInChildren<Transform>();

        for (int x = 0; x < allNavpoints.Length; x++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, allNavpoints[x].transform.position - transform.position, Vector3.Distance(allNavpoints[x].transform.position, transform.position));
            if (hit.collider != null)
            {
                print(hit.collider.name);
                continue;
            }
            else
            {
                if (allNavpoints[x].gameObject != gameObject)
                    nearest.Add(allNavpoints[x].GetComponent<Navpoint>());
            }
        }
        // Iterate through all navpoints in navmesh
        // Draw a ray to each navpoint
        // if the ray does not collide with a wall
        // add to the nearest list
    }
}
