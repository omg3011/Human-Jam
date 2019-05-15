using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;
    //acceleration speed(TESTING ONLY TO SEE THE SIMULATION, WILL CLEAN UP BEFORE SUBMITTING)
    public float ForwardSpeed = 400f;

    //LEFT RIGHT turn speed... will trial and error to make it more realistic
    public float LRSpeed = 800f;

    public float rotateSpeed = 3.0F;
    //rotation of x,y,z to look like player is making a turn...
    float rotx = 60f;
    float roty = 40f;
    float rotz = 10f;
    float minRotationX = -45, maxRotationX = 45;
    float currentAngle;
    float MaxForce = 500f;
    float rotationX, rotationY;

    // Update is called once per frame
    void FixedUpdate()
    {

        //Debug.Log(transform.eulerAngles.y);
        // left direction which is also input key 'A' 
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //transform in this case to rotate the player to turn left to be more realistic
            transform.Rotate(rotx * Time.deltaTime, 0, 0);
            rb.AddForce(-LRSpeed * Time.deltaTime, 0, 0);
            transform.Rotate(0, -roty * Time.deltaTime, 0);
        }

        // right direction which is also input key 'D' 
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            //transform in this case to rotate the player to turn right to be more realistic
      
            transform.Rotate(-rotx * Time.deltaTime, 0, 0);
            rb.AddForce(LRSpeed * Time.deltaTime, 0, 0);
            transform.Rotate(0, roty * Time.deltaTime, 0);

        }

        // Up direction which is also input key 'W' 
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rb.AddForce(0, 0, ForwardSpeed * Time.deltaTime);
        }

        // deceleration(TESTING ONLY TO SEE THE SIMULATION, WILL CLEAN UP BEFORE SUBMITTING)
        // direction which is also input key 'S' 
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rb.AddForce(0, 0, -ForwardSpeed * Time.deltaTime);
        }


    }

    // collision for between player and enemy
    void OnCollisionEnter(Collision col)
    {
        // if tagged is Enemy, then it will get the rigidbody component
        if (col.collider.tag == "Enemy")
        {
            Rigidbody enemyRB = col.gameObject.GetComponent<Rigidbody>();

            if (enemyRB)
            {
                // formula for calculating the collision between player and enemy. mat140...
                Vector3 dir = (enemyRB.transform.position - transform.position).normalized;
                enemyRB.AddForce(dir * 50, ForceMode.Impulse);
            }
        }
    }
}
