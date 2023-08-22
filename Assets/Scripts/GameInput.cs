using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerController _playerController;
    private void Awake()
    {
        _playerController = new PlayerController();
        _playerController.Player.Enable();
    }

    public Vector2 GetVector2Input()
    {
        Vector2 inputVector = _playerController.Player.Movement.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
