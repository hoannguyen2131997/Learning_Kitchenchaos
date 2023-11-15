using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    //[SerializeField] private GameObjectPool _gameObjectPool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //Application.targetFrameRate = 60;
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        PlatesObjectPool.Instance.CreatePlatesListObjectPool();
        RecipeTempletePool.Instance.CreateRecipeTempletePoolList();
    }
}
