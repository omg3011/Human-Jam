using UnityEngine;

//WORK IN PROGRESS...fk u collision
public class RigidBodyCollision : MonoBehaviour
{
    public Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter2D()
    {
        rb.AddForce(new Vector3(0,1,1));
    }
}
