using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAI : MonoBehaviour
{
    
    public float speed = 10.0f;
    private Vector3 moveDir;

    private Transform T_Player;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerGO = GameObject.FindWithTag("Player");

        // If found, reference its transform. (Safety check)
        if (playerGO)
        {
            T_Player = playerGO.transform;
        }
       // moveDir = (T_Player.transform.position - transform.position).normalized;
       // moveDir.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (T_Player)
        {
            // Direction to target
            moveDir = (T_Player.position - transform.position).normalized;

            // Got wall in between dont move
            //if (Check_For_Wall_Between_Me_And_Target_Part1(moveDir, 1000))      // Dont use this
            if (Check_For_Wall_Between_Me_And_Target_Part2(moveDir, 100))        // Use this
            {
                // Do nothing
            }
            else
            {
                transform.position += moveDir * speed * Time.deltaTime;
            }
        }
    }

    // Return true if, got wall
    bool Check_For_Wall_Between_Me_And_Target_Part2(Vector3 lookDir, float range)
    {
        // Raycast
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, lookDir, range);

        // Do this first, if we do hits[0] they will complain length 0, u check for what
        if (hits.Length > 0)
        {
            // Check if 1st ray collision is player, means no walls in between
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.tag == "Player")
                    return false;
                else if (hits[i].collider.tag == "ENEMY")
                    return true;
            }
        }

        // Check if no collider or if 1st ray collision is not player, means dont move lo.
        return true;
    }
}
