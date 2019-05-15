using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AiSpawner : MonoBehaviour
{
    public GameObject player;

    public GameObject idleAI, MoveAI, ChargeAI;
    public float amtOfAI;

    public float minX, maxX, minZ, maxZ;

    public void Start()
    {
        for(int i = 0; i < amtOfAI; i++)
        {
            int whichAI = Random.Range(0, 3);
            Vector3 pos = new Vector3(Random.Range(minX, maxX), 0.65f, Random.Range(minZ, maxZ) + 30);

            switch(whichAI)
            {
                case 0:
                    Instantiate(idleAI.transform, pos, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(MoveAI.transform, pos, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(ChargeAI.transform, pos, Quaternion.identity);
                    break;
            }
            
        }
    }
}
