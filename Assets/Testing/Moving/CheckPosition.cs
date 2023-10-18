using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPosition : MonoBehaviour
{
#if UNITY_EDITOR
    public static CheckPosition Instance;

    public Vector2 Vector2MoveDir;
    public Vector3 CheckPoint1;
    public Vector3 CheckPoint2;
    public Vector3 MoveForward;
    public bool CanMove;
    public bool IsWalking;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
#endif
}
