using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAI : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        int whichDir = Random.Range(0, 2);

        if (whichDir == 0)
            speed = -150.0f;
        else
            speed = 150.0f;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(speed * Time.deltaTime, 0, 0);
    }
}
