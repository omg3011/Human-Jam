using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EndlessEntity : MonoBehaviour
{

    //Reference
    [Header("<-- Spawn Points -->")]
    public Transform T_Next_Pattern_Spawn;    //where spawn next pattern?


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
}
