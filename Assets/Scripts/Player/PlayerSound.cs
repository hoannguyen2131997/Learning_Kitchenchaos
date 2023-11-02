using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private float footstepTimer;
    private float footstepTimerMax = .1f;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;
            if (_playerMovement.IsWalking())
            {
                SoundManager.Instance.PlayFootstepsSound(_playerMovement.transform.position);   
            }
        }
    }
}
