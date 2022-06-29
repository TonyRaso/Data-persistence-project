using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string NameToSave;
    public string NameInGame;
    public int HighScore;


    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();

    }


    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void AddName()
    {
        GameObject inputFieldGo = GameObject.Find("InputField");
        InputField inputFieldCo = inputFieldGo.GetComponent<InputField>();
        NameInGame = inputFieldCo.text;
    }

    [System.Serializable]
    class SaveData
    {
        public int HighScore;
        public string Name;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.HighScore = HighScore;
        data.Name = NameToSave;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScore = data.HighScore;
            NameToSave = data.Name;
        }
    }

}
