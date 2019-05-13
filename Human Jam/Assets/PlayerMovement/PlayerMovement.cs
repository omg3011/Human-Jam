using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float ForwardSpeed = 5000f;
    public float LRSpeed = 10000f;
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(0, 0, 200 * Time.deltaTime);

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-LRSpeed * Time.deltaTime, 0, 0);
        }
       
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.AddForce(LRSpeed * Time.deltaTime, 0, 0);
        }

        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rb.AddForce(0, 0 , ForwardSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rb.AddForce(0, 0, -ForwardSpeed * Time.deltaTime);
        }
    }
}
