using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    private enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    FryingObject();
                    break;
                case State.Fried:
                    BurningObject();
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    private void FryingObject()
    {
        fryingTimer += Time.deltaTime;
        if (fryingTimer > fryingRecipeSO.FryingTimerMax)
        {
            // Fried
            fryingTimer = 0f;
            Debug.Log("Fried!");
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(fryingRecipeSO.Output, this);
            
            Debug.Log("Object fried!");
            
            state = State.Fried;
            burningTimer = 0f;
            burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
        }
    }

    private void BurningObject()
    {
        burningTimer += Time.deltaTime;
        if (burningTimer > burningRecipeSO.BurningTimerMax)
        {
            // Fried
            burningTimer = 0f;
            Debug.Log("Fried!");
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(burningRecipeSO.Output, this);
            
            Debug.Log("Object burned!");
            state = State.Burned;
        }
    }

    public override void Interact(Player _player)
    {
        if (!HasKitchenObject())
        {
            // There is no KitchenObject here
            if (_player.HasKitchenObject())
            {
                // Player is carrying something
                Debug.Log(HasRecipeWithInput(_player.GetKitchenObject().GetKitchenObjectSO()));
                if (HasRecipeWithInput(_player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Player is carrying something that can be cut
                    _player.GetKitchenObject().SetKitchenObjectParent(this);
                    
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;
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
    
    private bool HasRecipeWithInput(KitchenObjectSO _inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSo = GetFryingRecipeSOWithInput(_inputKitchenObjectSO);
        return fryingRecipeSo != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO _inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSo = GetFryingRecipeSOWithInput(_inputKitchenObjectSO);
        if (fryingRecipeSo != null)
        {
            return fryingRecipeSo.Output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (FryingRecipeSO fryingRecipeSo in fryingRecipeSOArray)
        {
            if (fryingRecipeSo.Input == inputKitchenObjectSo)
            {
                return fryingRecipeSo;
            }
        }
        return null;
    }
    
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (BurningRecipeSO burningRecipeSo in burningRecipeSOArray)
        {
            if (burningRecipeSo.Input == inputKitchenObjectSo)
            {
                return burningRecipeSo;
            }
        }
        return null;
    }
}
