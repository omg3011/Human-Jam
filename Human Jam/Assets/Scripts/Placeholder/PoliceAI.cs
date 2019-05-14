using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private Transform T_player;
    private float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = (T_player.position - transform.position).normalized;
        moveDir.y = 0;

        transform.position += moveDir * speed * Time.deltaTime;
    }
}
