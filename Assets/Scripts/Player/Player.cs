using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public int coins;
    public float timer;
   
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        coins = data.coins;
        timer = data.timer;
    }
    
    public static Player Instance { get; private set; }
    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs: EventArgs
    {
        public BaseCounter selectedCounter;
    }
    
    [SerializeField] private GameInput gameInput;

    // lowering height raycast orgin to hit raycast for gameobject have lower height
    [SerializeField] private Vector3 loweringHeightRaycast;
    [SerializeField] private LayerMask couterLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_Oninteraction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternatetion;
        DeliveryManager.Instance.OnGetCoinPlayer += DeliveryManager_OnGetCoinPlayer;
    }

    private void DeliveryManager_OnGetCoinPlayer(object sender, DeliveryManager.OnGetCoinPlayerEventArgs e)
    {
        coins += e.coinsReceived;
        CollectionCoins(e.coinsReceived);
    }
    
    private async void CollectionCoins(int coin)
    {
        await CoinManagerUI.Instance.CollectionCoinsAnimation(coin);
    }

    private void GameInput_OnInteractAlternatetion(object sender, EventArgs e)
    {
        if(!KitchenGameManager.Instance.IsGamePlaying()) return;
        
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_Oninteraction(object sender, EventArgs e)
    {
        if(!KitchenGameManager.Instance.IsGamePlaying()) return;
        
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetVector2Input();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        float InteractDistance = 2f;
        //bool checkRaycastHit = Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, InteractDistance);
        if (Physics.Raycast(transform.position - loweringHeightRaycast, lastInteractDir, out RaycastHit raycastHit, InteractDistance))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                // Has ClearCounter
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs()
        {
            selectedCounter = selectedCounter
        });
    }

    #region Kitchen Object Interface Functions For Player
    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
    
    public void SetKitchenObject(KitchenObject _kitchenObject)
    {
        this.kitchenObject = _kitchenObject;
        if (_kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    #endregion
}