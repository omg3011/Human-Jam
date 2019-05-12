using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float SPEED_MULTIPLIER = 2f;
    public bool HasDamp;

    Vector3 targetPos;
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void LateUpdate()
    {
        if(target)
        {
            targetPos = target.transform.position;
            targetPos.x = startPos.x;
            targetPos.y = startPos.y;
            targetPos.z += startPos.z;

            if (HasDamp)
                transform.position = Vector3.Lerp(transform.position, targetPos, SPEED_MULTIPLIER * Time.deltaTime);
            else
                transform.position = targetPos;
        }
    }

}
