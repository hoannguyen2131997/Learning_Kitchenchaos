using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
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
    private int _StovingProgress;
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
        
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = (float)fryingTimer / fryingRecipeSO.FryingTimerMax
        });
        
        if (fryingTimer > fryingRecipeSO.FryingTimerMax)
        {
            // Fried
            fryingTimer = 0f;
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(fryingRecipeSO.Output, this);
            state = State.Fried;
            burningTimer = 0f;
            burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
            {
                state = state
            });
        }
    }

    private void BurningObject()
    {
        burningTimer += Time.deltaTime;
        
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = (float)burningTimer / burningRecipeSO.BurningTimerMax
        });
        
        if (burningTimer > burningRecipeSO.BurningTimerMax)
        {
            // Burned
            burningTimer = 0f;
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(burningRecipeSO.Output, this);
            state = State.Burned;
            
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
            {
                state = state
            });
        
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = 0f
            });
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
                    
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)fryingTimer / fryingRecipeSO.FryingTimerMax
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
                    
                    state = State.Idle;
                
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });
                }
            }
            else
            {
                // Player is not carrying anything
                
                GetKitchenObject().SetKitchenObjectParent(_player);

                state = State.Idle;
                
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });
                
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
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
