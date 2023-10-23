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
                Debug.Log($"Volumn's value sound foot: {SoundManager.Instance.GetVolumeSound()}");
                SoundManager.Instance.PlayFootstepsSound(_player.transform.position);   
            }
        }
    }
}
