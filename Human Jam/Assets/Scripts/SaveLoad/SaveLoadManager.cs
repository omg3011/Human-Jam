using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Player : MonoBehaviour
{

    public string Name;
    public int Level;
    public float Exp;
    public int HP;
    public int SP;
    public int STR;
    public int SPD;
    public int INT;
    public int CON;

    void Save()
    {
        JSONObject playerJson = new JSONObject();
        playerJson.Add("Name", Name);
        playerJson.Add("Level", Level);
        //BARS
        playerJson.Add("Exp", Exp);
        playerJson.Add("HP", HP);
        playerJson.Add("SP", SP);
        //STATS
        playerJson.Add("STR", STR);
        playerJson.Add("SPD", SPD);
        playerJson.Add("INT", INT);
        playerJson.Add("CON", CON);
        //POSITION
        JSONArray position = new JSONArray();
        position.Add(transform.position.x);
        position.Add(transform.position.y);
        position.Add(transform.position.z);
        playerJson.Add("Position", position);

        //SAVE JSON IN COMPUTER
        string path = Application.persistentDataPath + "/PlayerSave.json";
        File.WriteAllText(path, playerJson.ToString());

    }

    void Load()
    {
        string path = Application.persistentDataPath + "/PlayerSave.json";
        string jsonString = File.ReadAllText(path);
        JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);
        //SET VALUES
        Name = playerJson["Name"];
        Level = playerJson["Level"];
        //BARS
        Exp = playerJson["Exp"];
        HP = playerJson["HP"];
        SP = playerJson["SP"];
        //STATS
        STR = playerJson["STR"];
        SPD = playerJson["SPD"];
        INT = playerJson["INT"];
        CON = playerJson["CON"];
        //POSITION
        transform.position = new Vector3(
            playerJson["Position"].AsArray[0],
            playerJson["Position"].AsArray[1],
            playerJson["Position"].AsArray[2]
        );
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) Save();
        if (Input.GetKeyDown(KeyCode.L)) Load();
    }
}
*/