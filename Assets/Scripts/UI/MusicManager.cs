using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private float musicBegin = 0.3f;
    [SerializeField] private float musicUp = .1f;
    
    public static MusicManager Instance { get; private set; } 
    private float volume;
    private AudioSource _audioSource;
    private void Awake()
    {
        Instance = this;
        volume = musicBegin;
        _audioSource = GetComponent<AudioSource>();
    }

    public void ChangeMusicVolume()
    {
        volume += musicUp;
        if (volume > 1f)
        {
            volume = 0f;
        }
        _audioSource.volume = volume;
    }

    public float GetMusicVolume()
    {
        return volume;
    }
}
