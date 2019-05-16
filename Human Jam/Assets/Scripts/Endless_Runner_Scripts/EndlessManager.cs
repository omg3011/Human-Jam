using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessManager : Singleton<EndlessManager>
{
    // Twerkable
    public int initial_spawn_count = 10;

    // Private
    EndlessEntity latest_Pattern_Script;

    [Header("<-- Reference to first pattern-->")]
    public EndlessEntity first_Pattern_Script;

    void Start()
    {
        // Need to do this, to tell where is the first pattern->next spawn point
        latest_Pattern_Script = first_Pattern_Script;

        // Spawn "n" x Pattern
        for (int i = 0; i < initial_spawn_count; ++i)
        {
            SpawnNextPattern();
        }
    }

    void Update()
    {
        
    }

    public void SpawnPattern(PatternType type)
    {
        // Create Pattern
        GameObject go = ObjectPoolingManager.Instance.Spawn_A_Specific_PatternPrefab(type, Vector3.zero);

        // Spawn at latest->next 
        go.transform.position = latest_Pattern_Script.Get_Next_Pattern_Spawn_Position();

        // Record down new latest
        latest_Pattern_Script = go.GetComponent<EndlessEntity>();

    }

    public void SpawnNextPattern()
    {
        int randomPattern = Random.Range(0, 1);// (int)PatternType.TOTAL);

        SpawnPattern((PatternType) randomPattern);
    }

}
