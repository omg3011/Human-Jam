using System.Collections;
using UnityEngine;

//[ExecuteInEditMode]
public class SmoothFollow2 : MonoBehaviour
{
    // Reference
    public Transform target;

    // Twerkable
    public float distance = 3.0f;
    public float height = 3.0f;
    public float damping = 5.0f;
    public bool smoothRotation = true;
    public bool followBehind = true;
    public bool cameraDontFollow_X_Axis = true;
    public float rotationDamping = 10.0f;

    // private
    Vector3 wantedPosition;
    Quaternion wantedRotation;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void LateUpdate()
    {
        if (!target)
            return;

        if (followBehind)
            wantedPosition = target.position + new Vector3(0, height, -distance);
        else
            wantedPosition = target.TransformPoint(0, height, distance);

        if (cameraDontFollow_X_Axis)
            wantedPosition.x = target.position.x;
        else
            wantedPosition.x = startPosition.x;

        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);
    }
}
