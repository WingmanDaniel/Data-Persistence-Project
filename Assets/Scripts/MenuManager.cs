using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    private InputField NameInfo;
    private Text RankInfo;

    [System.Serializable]
    public struct PlayerInfo
    {
        public string PlayerName;
        public int PlayerScore;
    }

    public PlayerInfo BestPlayer;
    public PlayerInfo NewPlayer;



    private void Awake()
    {
        NameInfo = GameObject.Find("InputField").GetComponent<InputField>();
        RankInfo = GameObject.Find("RankInfo").GetComponent<Text>();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //LoadRankInfo();
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public PlayerInfo PlayerData;
    }


    public void UpdateNewPlayerData()
    {
        NewPlayer.PlayerName = NameInfo.text;
        NewPlayer.PlayerScore = 0;
    }

    public void LoadRankInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            BestPlayer.PlayerName = data.PlayerData.PlayerName;
            BestPlayer.PlayerScore = data.PlayerData.PlayerScore;

            RankInfo.text = "Best Score : " +BestPlayer.PlayerScore + " - " + BestPlayer.PlayerName;
        }
        else
        {
            BestPlayer.PlayerName = "N/A";
            BestPlayer.PlayerScore = 0;
        }
    }

    public void UpdatedBestPlayer()
    {
        if(NewPlayer.PlayerScore > BestPlayer.PlayerScore)
        {
            BestPlayer.PlayerScore = NewPlayer.PlayerScore;
            BestPlayer.PlayerName = NewPlayer.PlayerName;

            SaveData data = new SaveData();
            data.PlayerData.PlayerName = NewPlayer.PlayerName;
            data.PlayerData.PlayerScore = NewPlayer.PlayerScore;

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    } 
}
