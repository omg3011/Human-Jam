using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AIType
{
    IDLE,
    MOVE,
    CHARGE,
    TOTAL
}
public enum SpawnType
{
    RANDOM,
    IN_A_CIRCLE,
    RANGE,
    TOTAL
}

[System.Serializable]
public class EnemySpawnInfo
{
    public AIType ai_movement_type;
    public SpawnType ai_spawn_type;
    public CharacterType ai_character_type;
    public float ai_speed = 5;
    public int ai_spawn_count = 5;
    public List<Transform> T_CheckPoints;
}

[System.Serializable]
public class EndlessEntity : MonoBehaviour
{
    //Twerkable Values
    [Header("<-- Spawn Enemy Details -->")]
    public List<EnemySpawnInfo> enemySpawnDetails_list;

    //Reference
    [Header("<-- Spawn Points -->")]
    public Transform T_Next_Pattern_Spawn;    //where spawn next pattern?


    [Header("<-- Spawn Points -->")]
    public Transform T_Parent_EnemyList;

    //Private
    private List<Transform> ListOfEnemies;

    bool hasRun = false;
    

    void OnEnable()
    {
        if(hasRun)
            RestartAI();
    }
    private void Start()
    {
        if (!hasRun)
        {
            hasRun = true;

            SpawnEnemiesAtStart();
            RestartAI();
        }
    }

    void Update()
    {
        
    }

    //-- Initialize
    private void RestartAI()
    {
        int index = 0;

        for(int i = 0; i < enemySpawnDetails_list.Count; ++i)
        {
            int totalEnemyCount = enemySpawnDetails_list[i].ai_spawn_count;

            for(int z = 0; z < totalEnemyCount; ++z)
            {
                EnemySpawnInfo info = enemySpawnDetails_list[i];

                //RePosition enemy [Manual Position Enemy, move Way Point to Way Point]
                if (info.ai_spawn_type == SpawnType.RANGE && info.ai_movement_type == AIType.MOVE)
                {
                    // Spawn enemy at their CP1
                    ListOfEnemies[index].position = info.T_CheckPoints[0].position;
                }
                // Reposition Enemy: [Random spawn within a range, spawn idle enemy]
                else if(info.ai_spawn_type == SpawnType.RANDOM && info.ai_movement_type == AIType.IDLE)
                {
                    Vector3 randomPos = new Vector3(Random.Range(info.T_CheckPoints[0].position.x, info.T_CheckPoints[1].position.x),
                                                    0.5f,
                                                    Random.Range(info.T_CheckPoints[0].position.z, info.T_CheckPoints[1].position.z));
                    ListOfEnemies[index].position = randomPos;
                }
                // Reposition Enemy: [Random Spawn within a range, spawn charge enemy]
                else if(info.ai_spawn_type == SpawnType.RANDOM && info.ai_movement_type == AIType.CHARGE)
                {
                    Vector3 randomPos = new Vector3(Random.Range(info.T_CheckPoints[0].position.x, info.T_CheckPoints[1].position.x),
                                                    0.5f,
                                                    Random.Range(info.T_CheckPoints[0].position.z, info.T_CheckPoints[1].position.z));
                    ListOfEnemies[index].position = randomPos;
                }
                index++;
            }
        }
    }

    void SpawnEnemiesAtStart()
    {
        ListOfEnemies = new List<Transform>();
        // Loop through enemy list
        for (int i = 0; i < enemySpawnDetails_list.Count; ++i)
        {
            // No. of enemy to spawn
            int enemySpawnCount = enemySpawnDetails_list[i].ai_spawn_count;

            // Spawn Enemy
            for (int z = 0; z < enemySpawnCount; ++z)
            {
                // Variables
                GameObject enemyGo = null;

                // Spawn Enemy
                if (enemySpawnDetails_list[i].ai_character_type == CharacterType.PEOPLE)
                {
                    enemyGo = ObjectPoolingManager.Instance.Spawn_A_Specific_CharacterPrefab(CharacterType.PEOPLE, Vector3.zero);
                }
                else if (enemySpawnDetails_list[i].ai_character_type == CharacterType.POLICE)
                    enemyGo = ObjectPoolingManager.Instance.Spawn_A_Specific_CharacterPrefab(CharacterType.POLICE, Vector3.zero);

                if (enemyGo)
                {
                    // Assign enemy_cs T_CheckPoints
                    MoveAI ai_cs = enemyGo.GetComponent<MoveAI>();
                    if (ai_cs)
                    {
                        ai_cs.Init(enemySpawnDetails_list[i].ai_movement_type, enemySpawnDetails_list[i].T_CheckPoints, enemySpawnDetails_list[i].ai_speed);
                    }

                    // Add to list
                    ListOfEnemies.Add(enemyGo.transform);

                    // Parent it under a gameobject
                    enemyGo.transform.SetParent(T_Parent_EnemyList);
                }
            }
        }
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
