using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessManager : Singleton<EndlessManager>
{
    // Twerkable
    public int initial_spawn_count = 10;

    // Private
    EndlessEntity latest_Entity_Script;

    void Start()
    {
        // Spawn Default Pattern
        SpawnPattern(PatternType.PATTERN_START);

        // Spawn "n" x Pattern
        for (int i = 0; i < initial_spawn_count; ++i)
            SpawnNextPattern();
    }

    void Update()
    {
        
    }

    public void SpawnPattern(PatternType type)
    {
        GameObject go = ObjectPoolingManager.Instance.Spawn_A_Specific_PatternPrefab(type, Vector3.zero);

        // Spawn Start pattern
        if (type == PatternType.PATTERN_START)
        {
            // Spawn at position (0, 0, 0) 
            go.transform.position = Vector3.zero;

            // Record down script that is attach to this gameobject
            latest_Entity_Script = go.GetComponent<EndlessEntity>();

            // Spawn Player here too
            GameObject playerGO = ObjectPoolingManager.Instance.Spawn_A_Specific_CharacterPrefab(CharacterType.PLAYER, latest_Entity_Script.Get_Player_Spawn_Position());

            // Setup Camera here
            FindObjectOfType<SmoothFollow2>().target = playerGO.transform;
        }
        // Spawn Other Pattern
        else
        {
            // Spawn at latest->next 
            go.transform.position = latest_Entity_Script.Get_Next_Pattern_Spawn_Position();

            // Record down new latest
            latest_Entity_Script = go.GetComponent<EndlessEntity>();
        }

    }

    public void SpawnNextPattern()
    {
        int randomPattern = Random.Range(1, (int)PatternType.TOTAL);

        SpawnPattern((PatternType) randomPattern);
    }
}
