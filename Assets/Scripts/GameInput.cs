using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    public event EventHandler OnTest;
    
    private PlayerController _playerController;
    private void Awake()
    {
        _playerController = new PlayerController();
        _playerController.Player.Enable();

        _playerController.Player.Interact.performed += Interact_performed;
        _playerController.Player.InteractAlternate.performed += InteractAlternate;
        _playerController.Player.Test.performed += Test_performed;
    }

    private void Test_performed(InputAction.CallbackContext obj)
    {
        OnTest?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate(InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    
    public Vector2 GetVector2Input()
    {
        Vector2 inputVector = _playerController.Player.Movement.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
