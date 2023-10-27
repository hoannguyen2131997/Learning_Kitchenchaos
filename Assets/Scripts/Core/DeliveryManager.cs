using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler<OnRecipetCompleteAddedEventArgs> OnRecipeComplete;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    
    public class OnRecipetCompleteAddedEventArgs : EventArgs 
    {
        public int index;
    }
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO _recipeListSo;
    
    private List<RecipeSO> waitingRecipeSOList;
    private List<Transform> transRecipeSOList = new List<Transform>();
    private float spawnRecipeTimer = 3f;
    private float spawnRecipeTimerMax = 4f;
    private int wattingRecipesMax;
    private int successfulRecipesAmount;
    private int countItemCurrentRendered;
    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Start()
    {
        wattingRecipesMax = RecipeTempletePool.Instance.GetCountRecipeTempletePool();
        CreateListWattingRecipeSOList();
        transRecipeSOList = RecipeTempletePool.Instance.GetListTransRecipePool();
        AddDataForRecipeList();
    }

    private void AddDataForRecipeList()
    {
        for (int i = 0; i < wattingRecipesMax; i++)
        {
            transRecipeSOList[i].gameObject.GetComponent<DeliverySingleUI>().SetRecipeSO(waitingRecipeSOList[i]);
        }
    }

    public List<Transform> GetTransRecipeSOList()
    {
        return transRecipeSOList;
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (countItemCurrentRendered < wattingRecipesMax)
            {
                countItemCurrentRendered++;
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSos.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // Has the same number of ingredients
                bool plateContentMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSos)
                {
                    // Cycling through all ingredients in the Recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        // Cycling through all ingredients in the Plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            // Ingredient matches!
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        // This Recipe ingredient was not found on the Plate
                        plateContentMatchesRecipe = false;
                    }
                }

                if (plateContentMatchesRecipe)
                {
                    // Player delivered the correct recipe! 
                    successfulRecipesAmount++;
                    //waitingRecipeSOList.RemoveAt(i);
                    UpdateDataListRecipeSOItem(i);
                    Debug.Log(i);
                    OnRecipeComplete?.Invoke(this, new OnRecipetCompleteAddedEventArgs
                    {
                        index = i
                    });
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        
        // No matches found!
        // Player did not deliver a correct recipe 
        OnRecipeFailed?.Invoke(this,EventArgs.Empty);
    }

    public void UpdateDataListRecipeSOItem(int index)
    {
        waitingRecipeSOList[index] = _recipeListSo.recipeListSOs[Random.Range(0, _recipeListSo.recipeListSOs.Count)];
        transRecipeSOList[index].gameObject.GetComponent<DeliverySingleUI>().SetRecipeSO(waitingRecipeSOList[index]);
        countItemCurrentRendered--;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }
    
    private void CreateListWattingRecipeSOList()
    {
        int countWattingRecipeSOList = RecipeTempletePool.Instance.GetCountRecipeTempletePool();
        for (int i = 0; i < countWattingRecipeSOList; i++)
        {
            RecipeSO waitingRecipeSO = _recipeListSo.recipeListSOs[Random.Range(0, _recipeListSo.recipeListSOs.Count)];
            waitingRecipeSOList.Add(waitingRecipeSO);
        }
    }
}
