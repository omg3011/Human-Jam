using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolder_Player : MonoBehaviour
{
    public float speed = 10.0f;

    Vector3 inputDir;

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        //Read input
        inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
            
        transform.position += new Vector3(inputDir.x * speed * Time.deltaTime,
                                          0,
                                          speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "END_CHECK_POINT")
        {
            // Spawn a new Pattern
            EndlessManager.Instance.SpawnNextPattern();

            // Turn off the current pattern after a few second
            EndlessEntity entity = col.GetComponentInParent<EndlessEntity>();
            if(entity)
            {
                entity.Kill_Pattern_After_Delay(4);
            }
        }

        if (col.tag == "END_ENVIRONMENT_CP")
        {
            // Spawn a new Pattern
            EndlessManager.Instance.SpawnNextEnvironment();

            // Turn off the current pattern after a few second
            EndlessEntity entity = col.GetComponentInParent<EndlessEntity>();
            if (entity)
            {
                entity.Kill_Pattern_After_Delay(4);
            }
        }
    }
}
