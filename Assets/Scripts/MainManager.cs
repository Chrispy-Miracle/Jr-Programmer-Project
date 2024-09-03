using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    // getter makes main manager readonly to other scripts, private setter allows this script to modify it
    public static MainManager Instance { get; private set;} // now it is safe from outside world!

    public Color teamColor;

    private void Awake() {
        // this is a singleton pattern: ensure only 1 instance exists
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }


    [System.Serializable]  // allows JsonUtility to convert to json
    class SaveData {
        public Color teamColor;
    }


    public void SaveColor() {
        SaveData data = new SaveData();
        data.teamColor = teamColor;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);  // args: path, data
    }


    public void LoadColor() {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json); // convert to SaveData instance

            teamColor = data.teamColor;
        }
    }
}
