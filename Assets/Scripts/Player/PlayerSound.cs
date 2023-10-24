using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player _player;
    private float footstepTimer;
    private float footstepTimerMax = .1f;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;
            if (_player.IsWalking())
            {
                SoundManager.Instance.PlayFootstepsSound(_player.transform.position);   
            }
        }
    }
}
