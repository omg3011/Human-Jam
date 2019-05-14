using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceHolder_Player : MonoBehaviour
{
    public float speed = 10.0f;
    public float strafeSpeed = 5.0f;

    bool canMove = false;
    Vector3 inputDir;

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
            return;

        //Read input
        inputDir = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            
        transform.position += new Vector3(inputDir.x * strafeSpeed * Time.deltaTime,
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

        if(col.tag == "OBSTACLE")
        {
            SceneManager.LoadScene("Darren");
        }

        if (col.tag == "ENEMY")
        {
            SceneManager.LoadScene("Darren");
        }
    }

    public void StartMove()
    {
        canMove = true;
    }
}
