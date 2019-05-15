using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAI : MonoBehaviour
{
    // Twerkable
    public float speed;

    // Reference
    public AIType aiMovementType;
    public List<Transform> ListOfCP;

    // Hidden
    [HideInInspector] public Vector3 moveDir;

    // Const
    const float GAP_THRESHOLD = 0.5f;

    // Private
    Transform T_Player;
    Rigidbody rb;
    int cp_index;
    Vector3 targetPos;

    bool hasCharged = false;
    float nextHomFinish;
    const float HOM_THRESHOLD = 1.0f;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        // Find Player
        if(T_Player == null)
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO)
                T_Player = playerGO.transform;
        }

        if(aiMovementType == AIType.CHARGE)
        {
            hasCharged = false;
        }


        cp_index = 0;

        //int whichDir = Random.Range(0, 2);

        //if (whichDir == 0)
        //    speed = -150.0f;
        //else
        //    speed = 150.0f;
    }

    public void Init(AIType ai_type, List<Transform> ListCP, float newSpeed)
    {
        aiMovementType = ai_type;
        ListOfCP = ListCP;
        cp_index = 0;
        targetPos = GetTargetPos();
        speed = newSpeed;

        // Update new rotation
        ChangeRotation(targetPos);
    }

    Vector3 GetTargetPos()
    {
        int targetIndex = cp_index+1;
        if (targetIndex >= ListOfCP.Count)
            targetIndex = 0;
        return ListOfCP[targetIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        if(aiMovementType == AIType.MOVE)
        {
            float distanceBetweenCP = Vector3.Distance(targetPos, transform.position);
            if (distanceBetweenCP < GAP_THRESHOLD)
            {
                // Get the new index
                cp_index++;
                if (cp_index >= ListOfCP.Count)
                    cp_index = 0;
                targetPos = GetTargetPos();

                // Update new rotation
                ChangeRotation(targetPos);
            }
            else
            {
                moveDir = (targetPos - ListOfCP[cp_index].position).normalized;
                transform.position += moveDir * speed * Time.deltaTime;
                //rb.AddForce(speed * Time.deltaTime, 0, 0);
            }
        }
        else if(aiMovementType == AIType.CHARGE)
        {
            // If player exist,
            if(T_Player)
            {
                // Havent charge, because got wall in between player and enemy
                if(!hasCharged)
                {
                    // Update Rotation: keep facing player
                    moveDir = (T_Player.position - transform.position).normalized;
                    moveDir.y = 0;
                    if(moveDir != Vector3.zero)
                        ChangeRotation(T_Player.position);

                    // Got: wall in between player and enemy [Therefore, dont move]
                    if (Check_For_Wall_Between_Me_And_Target_Part2(moveDir, 1000))        // Use this
                    {
                        // Do Nothing
                    }
                    else
                    {
                        hasCharged = true;
                        nextHomFinish = Time.time + HOM_THRESHOLD;
                    }
                }
                // Start charging
                else
                {
                    // No hom after 2s
                    if (Time.time > nextHomFinish)
                    {
                        transform.position += moveDir * speed * Time.deltaTime;
                    }
                    // Hom towards player for 0.5s
                    else
                    {
                        // Apply Rotation: keep facing player
                        moveDir = (T_Player.position - transform.position).normalized;
                        moveDir.y = 0;
                        if (moveDir != Vector3.zero)
                            ChangeRotation(T_Player.position);

                        // Apply Movement
                        transform.position += moveDir * speed * Time.deltaTime;
                    }
                }
            }
        }

    }

    void ChangeRotation(Vector3 target)
    {
        // Rotate correct direction
        Vector3 rotDir = (target - transform.position).normalized;
        rotDir.y = 0;
        if(rotDir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(rotDir);
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
                else if (hits[i].collider.tag == "OBSTACLE")
                    return true;
            }
        }

        // Check if no collider or if 1st ray collision is not player, means dont move lo.
        return true;
    }
}
