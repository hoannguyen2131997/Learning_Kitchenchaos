using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesObjectPool : MonoBehaviour
{
    public static PlatesObjectPool Instance;

    public int CountPlates;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private Transform transformPlatesObjectPool;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    
    private Dictionary<int, Transform> PlatesObjectPoolList = new Dictionary<int, Transform>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CreatePlatesListObjectPool()
    {
        if (CountPlates != 0)
        {
            for (int i = 0; i < CountPlates; i++)
            {
                Transform plateTransform = Instantiate(plateVisualPrefab, transformPlatesObjectPool);
                plateTransform.gameObject.SetActive(false);
                PlatesObjectPoolList.Add(i, plateTransform);
            }
        }
        else
        {
            Debug.LogError("CountPlates of GameObjectPool class is 0");
        }
    }

    public Transform GetPlateItem(int count)
    {
        Transform plateObject = PlatesObjectPoolList[count].transform;
        return plateObject;
    }

    public void PushPlateItem(int count)
    {
        PlatesObjectPoolList[count].transform.SetParent(transformPlatesObjectPool);
        PlatesObjectPoolList[count].gameObject.SetActive(false);
    }
}
