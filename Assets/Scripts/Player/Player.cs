using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs: EventArgs
    {
        public BaseCounter selectedCounter;
    }
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameInput gameInput;

    // lowering height raycast orgin to hit raycast for gameobject have lower height
    [SerializeField] private Vector3 loweringHeightRaycast;
    [SerializeField] private LayerMask couterLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    
    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    // Check point
    private Vector3 CheckPoint1;
    private Vector3 CheckPoint2;
    private bool canMove;

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
        gameInput.OnTest += GameInput_OnTest;
    }

    private void GameInput_OnTest(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.TestInteract(this);
        }
    }

    private void GameInput_OnInteractAlternatetion(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_Oninteraction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        CheckPoint();
        HandleInteraction();
    }

    public bool IsWalking()
    {
        return isWalking;
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

    private void CheckPoint()
    {
        Vector2 inputVector = gameInput.GetVector2Input();
        CheckPosition.Instance.Vector2MoveDir.x = inputVector.x;
        CheckPosition.Instance.Vector2MoveDir.y = inputVector.y;

        CheckPosition.Instance.CheckPoint1 = CheckPoint1;
        CheckPosition.Instance.CheckPoint2 = CheckPoint2;
        CheckPosition.Instance.MoveForward = transform.forward;
        CheckPosition.Instance.CanMove = canMove;
        CheckPosition.Instance.IsWalking = isWalking;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetVector2Input();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerHeight = 2f;
        float playerRadius = .7f;
        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        CheckPoint1 = transform.position;
        CheckPoint2 = transform.position + Vector3.up * playerHeight;
        if (!canMove)
        {
            // Cannot move towards moveDir
            
            // Attempt only X movement, moveDir.x != 0 mean player only interact when input player is forward counter, example when player capsule cast with counter : (1,0) -> interact, but (0,1) not trigger interact
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(CheckPoint1, CheckPoint2, playerRadius, moveDir, moveDistance);

            if (canMove)
            {
                // Can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                // Cannot move only on the X
                
                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(CheckPoint1, CheckPoint2, playerRadius, moveDir, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    // Cannot move in any direction
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        isWalking = moveDir != Vector3.zero;
        //transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
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