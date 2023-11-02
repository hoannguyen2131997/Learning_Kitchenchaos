using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    
    // Check point
    private Vector3 CheckPoint1;
    private Vector3 CheckPoint2;
    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;
    
    [SerializeField, Tooltip("Input Speed smooth damp speed")] 
    private float smoothInputSpeed = .2f;
    private bool isWalking;
    private bool canMove;
    private void Update()
    {
        HandleMovement();
    }
    
    public bool IsWalking()
    {
        return isWalking;
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetVector2Input();
        currentInputVector = Vector2.SmoothDamp(currentInputVector, inputVector, ref smoothInputVelocity, smoothInputSpeed);
        Vector3 moveDir = new Vector3(currentInputVector.x, 0f, currentInputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerHeight = 2f;
        float playerRadius = .7f;
        moveDir.Normalize();
        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        CheckPoint1 = transform.position;
        CheckPoint2 = transform.position + Vector3.up * playerHeight;
        if (!canMove)
        {
            // Cannot move towards moveDir
            // Attempt only X movement, moveDir.x != 0 mean player only interact when input player is forward counter, example when player capsule cast with counter : (1,0) -> interact, but (0,1) not trigger interact
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
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
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
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
        if (transform.forward != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
    }
}
