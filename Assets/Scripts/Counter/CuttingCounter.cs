using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int _cuttingProgress;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;
    public static event EventHandler OnAnyCut;

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
                    // Player is carrying something that can be cut
                    _player.GetKitchenObject().SetKitchenObjectParent(this);
                    _cuttingProgress = 0;
                    
                    CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)_cuttingProgress / cuttingRecipeSo.cuttingProgressMax
                    });
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
                if (_player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player is holding a Plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
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
            // There is a KitchenObject here and it can be cut
            _cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            
            CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)_cuttingProgress / cuttingRecipeSo.cuttingProgressMax
            });
            
            if (_cuttingProgress >= cuttingRecipeSo.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    public override void TestInteract(Player _player)
    {
        if (HasKitchenObject())
        {
            Debug.Log("Hoan - Test Interact");
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO _inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSOWithInput(_inputKitchenObjectSO);
        return cuttingRecipeSo != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO _inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSOWithInput(_inputKitchenObjectSO);
        if (cuttingRecipeSo != null)
        {
            return cuttingRecipeSo.Output;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (var cuttingRecipeSo in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSo.Input == inputKitchenObjectSo)
            {
                return cuttingRecipeSo;
            }
        }
        return null;
    }
}
