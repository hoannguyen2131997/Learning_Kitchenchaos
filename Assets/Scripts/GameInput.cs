using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    private PlayerController _playerController;
    private void Awake()
    {
        _playerController = new PlayerController();
        _playerController.Player.Enable();

        _playerController.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    
    public Vector2 GetVector2Input()
    {
        Vector2 inputVector = _playerController.Player.Movement.ReadValue<Vector2>();
        return inputVector;
    }
}
