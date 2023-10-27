using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliVeryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplete;
   
    private void Awake()
    {
        recipeTemplete.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeComplete += DeliveryManager_OnRecipeComplete;
    }

    private void DeliveryManager_OnRecipeComplete(object sender,  DeliveryManager.OnRecipetCompleteAddedEventArgs e)
    {
        int n = DeliveryManager.Instance.GetTransRecipeSOList().Count;
        for (int i = 0; i < n; i++)
        {
            if (i == e.index)
            {
                DeliveryManager.Instance.GetTransRecipeSOList()[i].gameObject.SetActive(false);
                DeliveryManager.Instance.GetTransRecipeSOList()[i].SetParent(RecipeTempletePool.Instance.GetTrasParent());
                return;
            }
        }
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        foreach (Transform transObjectItem in DeliveryManager.Instance.GetTransRecipeSOList())
        {
            if (transObjectItem.gameObject.activeSelf == false)
            {
                transObjectItem.SetParent(container);
                transObjectItem.gameObject.SetActive(true);
                return;
            }
        }
    }
}
