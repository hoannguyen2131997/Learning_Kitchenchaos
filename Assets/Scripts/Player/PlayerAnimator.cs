using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private const String IS_WALKING = "IsWalking";
    [SerializeField] private PlayerMovement playerMovement;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, playerMovement.IsWalking());
    }
}
