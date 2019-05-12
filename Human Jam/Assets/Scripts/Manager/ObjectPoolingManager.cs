using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    PLAYER,
    NPC_1,
    NPC_2,
    TOTAL
}
public enum PatternType
{
    PATTERN_START,
    PATTERN_1,
    PATTERN_2,
    TOTAL
}
public enum EnvironmentType
{
    ENVIRONMENT_1,
    TOTAL
}

[System.Serializable]
public class PooledObject
{
    public GameObject Object;
    public int Amount;
}

public class ObjectPoolingManager : Singleton<ObjectPoolingManager>
{
    // Reference
    public PooledObject[] CharacterObjects, PatternObjects, EnvironmentObjects;

    // Private
    List<GameObject>[] characterPool_List, patternPool_List, environmentPool_List;

    //----------------------------------------------------------//
    //                          SETUP                           //
    //----------------------------------------------------------//
    void Awake()
    {
        SetupPooling();
    }
    void SetupPooling()
    {
        GameObject temp;

        //----------------------------//
        //      [POOLING]: Character  //
        //----------------------------//
        characterPool_List = new List<GameObject>[CharacterObjects.Length];

        for(int count = 0; count < CharacterObjects.Length; count++)
        {
            characterPool_List[count] = new List<GameObject>();
            
            for(int num = 0; num < CharacterObjects[count].Amount; num++)
            {
                temp = (GameObject)Instantiate(CharacterObjects[count].Object, new Vector3(0, 100, 0), CharacterObjects[count].Object.transform.rotation);
                temp.SetActive(false);
                temp.transform.parent = transform;
                characterPool_List[count].Add(temp);
            }
        }

        //----------------------------//
        //      [POOLING]: PATTERN    //
        //----------------------------//
        patternPool_List = new List<GameObject>[PatternObjects.Length];

        for (int count = 0; count < PatternObjects.Length; count++)
        {
            patternPool_List[count] = new List<GameObject>();

            for (int num = 0; num < PatternObjects[count].Amount; num++)
            {
                temp = (GameObject)Instantiate(PatternObjects[count].Object, new Vector3(0, 100, 0), PatternObjects[count].Object.transform.rotation);
                temp.SetActive(false);
                temp.transform.parent = transform;
                patternPool_List[count].Add(temp);
            }
        }

        //--------------------------------//
        //      [POOLING]: ENVIRONMENT    //
        //--------------------------------//
        environmentPool_List = new List<GameObject>[EnvironmentObjects.Length];

        for (int count = 0; count < EnvironmentObjects.Length; count++)
        {
            environmentPool_List[count] = new List<GameObject>();

            for (int num = 0; num < EnvironmentObjects[count].Amount; num++)
            {
                temp = (GameObject)Instantiate(EnvironmentObjects[count].Object, new Vector3(0, 100, 0), EnvironmentObjects[count].Object.transform.rotation);
                temp.SetActive(false);
                temp.transform.parent = transform;
                environmentPool_List[count].Add(temp);
            }
        }
    }

    //----------------------------------------------------------//
    //                        UTILITY                           //
    //----------------------------------------------------------//
    //- Specific
    public GameObject Spawn_A_Specific_CharacterPrefab(CharacterType type, Vector3 newPosition)
    {
        int characterID = (int)type;

        // If we have unused gameobject in our pool, we reuse
        for(int count = 0; count < characterPool_List[characterID].Count; count++)
        {
            if(!characterPool_List[characterID][count].activeSelf)
            {
                GameObject currObj = characterPool_List[characterID][count];
                currObj.SetActive(true);
                currObj.transform.position = newPosition;
                return currObj;
            }
        }

        // If not we have "NO UN-USED" gameobject in our pool, we create new gameobject and add into pool list
        GameObject newObj = Instantiate(CharacterObjects[characterID].Object) as GameObject;
        newObj.transform.position = newPosition;
        characterPool_List[characterID].Add(newObj);
        return newObj;
    }

    //- Specific
    public GameObject Spawn_A_Specific_PatternPrefab(PatternType patternType, Vector3 newPosition)
    {
        int patternID = (int)patternType;

        // If we have unused gameobject in our pool, we reuse
        for (int count = 0; count < patternPool_List[patternID].Count; count++)
        {
            if (!patternPool_List[patternID][count].activeSelf)
            {
                GameObject currObj = patternPool_List[patternID][count];
                currObj.SetActive(true);
                currObj.transform.position = newPosition;
                return currObj;
            }
        }

        // If not we have "NO UN-USED" gameobject in our pool, we create new gameobject and add into pool list
        GameObject newObj = Instantiate(PatternObjects[patternID].Object) as GameObject;
        newObj.transform.position = newPosition;
        patternPool_List[patternID].Add(newObj);
        return newObj;
    }

    //- Specific
    public GameObject Spawn_A_Specific_EnvironmentPrefab(EnvironmentType environmentType, Vector3 newPosition)
    {
        int environmentID = (int)environmentType;

        // If we have unused gameobject in our pool, we reuse
        for (int count = 0; count < environmentPool_List[environmentID].Count; count++)
        {
            if (!environmentPool_List[environmentID][count].activeSelf)
            {
                GameObject currObj = environmentPool_List[environmentID][count];
                currObj.SetActive(true);
                currObj.transform.position = newPosition;
                return currObj;
            }
        }

        // If not we have "NO UN-USED" gameobject in our pool, we create new gameobject and add into pool list
        GameObject newObj = Instantiate(EnvironmentObjects[environmentID].Object) as GameObject;
        newObj.transform.position = newPosition;
        environmentPool_List[environmentID].Add(newObj);
        return newObj;
    }

    //- Random
    public GameObject Spawn_A_Random_CharacterPrefab(Vector3 newPosition)
    {
        int characterID = Random.Range(0, (int)CharacterType.TOTAL);

        // If we have unused gameobject in our pool, we reuse
        for (int count = 0; count < characterPool_List[characterID].Count; count++)
        {
            if (!characterPool_List[characterID][count].activeSelf)
            {
                GameObject currObj = characterPool_List[characterID][count];
                currObj.SetActive(true);
                currObj.transform.position = newPosition;
                return currObj;
            }
        }

        // If not we have "NO UN-USED" gameobject in our pool, we create new gameobject and add into pool list
        GameObject newObj = Instantiate(CharacterObjects[characterID].Object) as GameObject;
        newObj.transform.position = newPosition;
        characterPool_List[characterID].Add(newObj);
        return newObj;
    }

    //- Random
    public GameObject Spawn_A_Random_PatternPrefab(Vector3 newPosition)
    {
        int patternID = Random.Range(0, (int)PatternType.TOTAL);

        // If we have unused gameobject in our pool, we reuse
        for (int count = 0; count < patternPool_List[patternID].Count; count++)
        {
            if (!patternPool_List[patternID][count].activeSelf)
            {
                GameObject currObj = patternPool_List[patternID][count];
                currObj.SetActive(true);
                currObj.transform.position = newPosition;
                return currObj;
            }
        }

        // If not we have "NO UN-USED" gameobject in our pool, we create new gameobject and add into pool list
        GameObject newObj = Instantiate(PatternObjects[patternID].Object) as GameObject;
        newObj.transform.position = newPosition;
        patternPool_List[patternID].Add(newObj);
        return newObj;
    }

    //- Random
    public GameObject Spawn_A_Random_EnvironmentPrefab(Vector3 newPosition)
    {
        int environmentID = Random.Range(0, (int)EnvironmentType.TOTAL);

        // If we have unused gameobject in our pool, we reuse
        for (int count = 0; count < environmentPool_List[environmentID].Count; count++)
        {
            if (!environmentPool_List[environmentID][count].activeSelf)
            {
                GameObject currObj = environmentPool_List[environmentID][count];
                currObj.SetActive(true);
                currObj.transform.position = newPosition;
                return currObj;
            }
        }

        // If not we have "NO UN-USED" gameobject in our pool, we create new gameobject and add into pool list
        GameObject newObj = Instantiate(EnvironmentObjects[environmentID].Object) as GameObject;
        newObj.transform.position = newPosition;
        environmentPool_List[environmentID].Add(newObj);
        return newObj;
    }

}
