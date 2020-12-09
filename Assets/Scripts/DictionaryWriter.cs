using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerScore
{
    public string name;
    public int score;
    public PlayerScore(string name, int newScore)
    {
        this.name = name;
        this.score = newScore;
    }
}

public class DictionaryWriter : MonoBehaviour
{
    public static DictionaryWriter Instance;

    private string path;
    private PlayerScore[] playersArray;
    public PlayerScore player;
    public List<PlayerScore> players;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;

            players = new List<PlayerScore>();
            path = Application.persistentDataPath + "/scores.json";
            Debug.Log(path);
            
            if (!File.Exists(path)) File.WriteAllText(path, "");
            
            UpdateScores();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void WriteScore(string name, int newScore)
    {
        PlayerScore myObject = new PlayerScore(name, newScore);
        string json = JsonUtility.ToJson(myObject);
        File.AppendAllText(path, json);        
    }

    public void UpdateScores()
    {
        players.Clear();
        
        string jsonString = File.ReadAllText(path);
        var strings = jsonString.Split('}');
        foreach (var singleString in strings)
        {
            if (singleString.Length < 2) break;
            var newSingleString = singleString + "}";
            players.Add(JsonUtility.FromJson<PlayerScore>(newSingleString));
        }

        playersArray = players.ToArray();
        players = playersArray.OrderByDescending(p => p.score).ToList();
    }
}


