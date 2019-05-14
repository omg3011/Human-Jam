using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAI : MonoBehaviour
{
    public GameObject T_player;
    public float speed = 10.0f;
    private Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        T_player = GameObject.FindWithTag("Player");
        moveDir = (T_player.transform.position - transform.position).normalized;
        moveDir.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDir * speed * Time.deltaTime;
    }
}
