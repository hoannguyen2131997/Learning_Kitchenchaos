using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(Player _player)
    {
        if (!HasKitchenObject())
        {
            // There is no KitchenObject here
            if (_player.HasKitchenObject())
            {
                // Player is carrying something
                if (HasRecipeWithInput(_player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Player is carrying something
                    _player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
            else
            {
                // Player not carrying anything
            }
        }
        else
        {
            // There is a KitchnObject here
            if (_player.HasKitchenObject())
            {
                // Player is carrying something
            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(_player);
            }
        }
    }

    public override void InteractAlternate(Player _player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            // There is a KitchenObject here
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    public override void TestInteract(Player _player)
    {
        if (HasKitchenObject())
        {
            Debug.Log("Hoan - Test Interact");
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.Input == inputKitchenObjectSO)
            {
                return true;
            }
        }
        return false;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.Input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO.Output;
            }
        }
        return null;
    }
}
