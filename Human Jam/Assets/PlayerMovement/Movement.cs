using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float ForwardSpeed = 500f;
    public float LRSpeed = 1000f;
    // Update is called once per frame
    void FixedUpdate()
    {
       // rb.AddForce(0, 0, 200 * Time.deltaTime);

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-LRSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.AddForce(LRSpeed * Time.deltaTime, 0, 0);
        }
    }
}
