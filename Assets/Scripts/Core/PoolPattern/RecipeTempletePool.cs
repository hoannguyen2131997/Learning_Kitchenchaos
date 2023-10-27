using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeTempletePool : MonoBehaviour
{
    public static RecipeTempletePool Instance;
    [SerializeField] private Transform recipeTemplete;
    [SerializeField] private Transform transformRecipeTemplete;
    [SerializeField] private int countRecipeTemplete;
    private List<Transform> recipeTempletePoolList = new List<Transform>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CreateRecipeTempletePoolList()
    {
        for (int i = 0; i < countRecipeTemplete; i++)
        {
            Transform recipeTempleteObject = Instantiate(recipeTemplete, transformRecipeTemplete);
            recipeTempleteObject.gameObject.SetActive(false);
            recipeTempletePoolList.Add(recipeTempleteObject);
        }
    }

    public int GetCountRecipeTempletePool()
    {
        return countRecipeTemplete;
    }

    public Transform GetTrasParent()
    {
        return transformRecipeTemplete;
    }

    public List<Transform> GetListTransRecipePool()
    {
        return recipeTempletePoolList;
    }
}
