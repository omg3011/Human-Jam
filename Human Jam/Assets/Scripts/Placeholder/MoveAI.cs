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
    const float GAP_THRESHOLD = 2.0f;

    // Private
    Rigidbody rb;
    int cp_index;
    Vector3 targetPos;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        if(aiMovementType == AIType.MOVE)
        {
            if(ListOfCP.Count > 0)
                transform.position = ListOfCP[0].position;
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
        ChangeRotation();
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
                ChangeRotation();
            }
            else
            {
                moveDir = (targetPos - ListOfCP[cp_index].position).normalized;
                transform.position += moveDir * speed * Time.deltaTime;
                //rb.AddForce(speed * Time.deltaTime, 0, 0);
            }
        }

    }

    void ChangeRotation()
    {
        // Rotate correct direction
        Vector3 rotDir = (targetPos - transform.position).normalized;
        rotDir.y = 0;
        if(rotDir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(rotDir);
    }
}
