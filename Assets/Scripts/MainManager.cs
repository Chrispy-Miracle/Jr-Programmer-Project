using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    // make main manager accessible from other scripts
    public static MainManager Instance;

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
