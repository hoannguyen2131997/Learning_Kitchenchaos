using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using File = System.IO.File;

public class DataLoadSave : MonoBehaviour
{
    public static DataLoadSave Instance;
    public GameObject PlayerGameObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void LoadData()
    {
        string json = File.ReadAllText(Application.dataPath + "/playerData.json");
        DataGamePlay data = JsonUtility.FromJson<DataGamePlay>(json);

        Player.Instance.SetCoinPlayer(data.CoinsUser);
    }

    public void SaveData()
    {
        DataGamePlay data = new DataGamePlay();
        data.CoinsUser = Player.Instance.GetCoinPlayer();

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + "/playerData.json", json);
    }
}
