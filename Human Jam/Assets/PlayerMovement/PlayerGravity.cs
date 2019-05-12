
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
