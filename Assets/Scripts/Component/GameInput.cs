using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnTest;
    public event EventHandler OnPause;
    
    private PlayerController _playerController;
    private void Awake()
    {
        Instance = this;
        
        
        _playerController = new PlayerController();
        _playerController.Player.Enable();

        _playerController.Player.Interact.performed += Interact_performed;
        _playerController.Player.InteractAlternate.performed += InteractAlternate;
        _playerController.Player.Test.performed += Test_performed;
        _playerController.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        _playerController.Player.Interact.performed -= Interact_performed;
        _playerController.Player.InteractAlternate.performed -= InteractAlternate;
        _playerController.Player.Test.performed -= Test_performed;
        _playerController.Player.Pause.performed -= Pause_performed;
        
        _playerController.Dispose();
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPause?.Invoke(this, EventArgs.Empty);
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
        Vector2 inputVectorMove = _playerController.Player.Movement.ReadValue<Vector2>();
        Vector2 inputVectorLeftJoystick = _playerController.Player.JoystickLeft.ReadValue<Vector2>();
        
        if (inputVectorMove != Vector2.zero)
        {
            //inputVectorMove = inputVectorMove.normalized;
            return inputVectorMove;
        }
        
        //inputVectorLeftJoystick = inputVectorLeftJoystick.normalized;
        return inputVectorLeftJoystick;
    }
}
