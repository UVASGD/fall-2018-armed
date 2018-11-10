using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroState : AIState {

    float endTime;
    GameObject player;
    Transform gunHolder;
    Vector3 lastPlayerPosition;

    public AggroState(EnemyMovement enemy)
    {
        currEnemy = enemy;
        endTime = Time.time + enemy.AggroTime;
        player = GameObject.Find("Player");
        gunHolder = currEnemy.transform.GetChild(0);
    }

    override
    public void MoveBasedOnState()
    {
        RaycastHit2D hit = Physics2D.Raycast(currEnemy.transform.position, player.transform.position - currEnemy.transform.position);
        if (hit.collider.name == "Player")
        {
            float angle = Vector2.SignedAngle(Vector2.up, player.transform.position - currEnemy.transform.position);
            currEnemy.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));  //Rotate Player
            lastPlayerPosition = player.transform.position;
            foreach (Transform child in gunHolder)
            {
                child.GetComponent<Gun>().Shoot();
            }
            int res = Random.Range(0, 10);
            if (res < 7)
            {
                // The step size is equal to speed times frame time.
                float step = currEnemy.speed * Time.deltaTime;
                // Move our position a step closer to the target.
                currEnemy.transform.position = Vector3.MoveTowards(currEnemy.transform.position, lastPlayerPosition, step);
            }
        }
        else
        {
            // The step size is equal to speed times frame time.
            float step = currEnemy.speed * Time.deltaTime;
            // Move our position a step closer to the target.
            currEnemy.transform.position = Vector3.MoveTowards(currEnemy.transform.position, lastPlayerPosition, step);
        }

        if (Time.time > endTime)
        {
            currEnemy.setState(new AlertState(currEnemy));
        }
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
