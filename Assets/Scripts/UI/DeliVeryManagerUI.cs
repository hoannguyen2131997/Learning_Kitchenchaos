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
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeComplete(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if(child == recipeTemplete) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSo in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplete, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.gameObject.GetComponent<DeliverySingleUI>().SetRecipeSO(recipeSo);
        }
    }
}
