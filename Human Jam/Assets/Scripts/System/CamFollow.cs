using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [HideInInspector]
    public Transform target;
    public Vector3 Dist_From_Target;

    void Start()
    {
    }

    void LateUpdate()
    {
        if (target)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z - Dist_From_Target.z);
        }
    }

}
