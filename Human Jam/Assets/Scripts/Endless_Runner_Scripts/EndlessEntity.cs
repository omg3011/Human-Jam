using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessEntity : MonoBehaviour
{

    //Reference
    [Header("<-- Spawn Points -->")]
    public Transform T_Next_Pattern_Spawn;    //where spawn next pattern?
    public Transform T_Player_Spawn;          //where spawn player?

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //-- Utility
    public void Kill_Pattern_After_Delay(float seconds)
    {
        StartCoroutine(Start_Kill_Pattern(seconds));
    }

    //-- Callback
    IEnumerator Start_Kill_Pattern(float seconds)
    {
        // Delay after seconds
        yield return new WaitForSeconds(seconds);

        // After delay, turn off this gameobject
        this.gameObject.SetActive(false);
    }

    //--- Getter
    public Vector3 Get_Next_Pattern_Spawn_Position()
    {
        return T_Next_Pattern_Spawn.position;
    }

    public Vector3 Get_Player_Spawn_Position()
    {
        return T_Player_Spawn.position;
    }
}
