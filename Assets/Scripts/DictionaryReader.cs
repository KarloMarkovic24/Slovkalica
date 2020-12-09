using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DictionaryReader : MonoBehaviour
{
    public static DictionaryReader Instance;

    private string path;
    public Dictionary<string, string[]> dictionary;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;

            path = Application.dataPath + "/dictionary.json";
            string jsonString = File.ReadAllText(path);
            dictionary = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(jsonString);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }  
    }
}
