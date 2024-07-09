using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 SavedPos;
    public int CheckPointNo;
}

public class CheckPointMng : MonoBehaviour
{
    public GameData gameData;
    private string saveFilePath;

    public GameObject[] CheckPoints;

    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "gamedata.json");
        gameData = LoadGameData();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGameData(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game data saved.");
    }

    public GameData LoadGameData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Game data loaded.");
            return data;
        }
        else
        {
            Debug.LogWarning("No save file found, initializing with default values.");
            return new GameData();
        }
    }

    public void Save()
    {
        SaveGameData(gameData);
    }

    public void NewGame()
    {
        SaveGameData(new GameData());
    }
}
