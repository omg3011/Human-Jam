
using UnityEngine;

public class DummyGravity : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.useGravity = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
