using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveUser : MonoBehaviour
{
    public static SaveUser Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SaveUserVolumeMusic()
    {
        SoundManager.Instance.GetVolumeSound();
    }

    public void SaveUserVolumeSound()
    {
        MusicManager.Instance.GetMusicVolume();
    }
    
    /*
      public void SaveUserCoin(int coin)
    {
        CoinManagerUI.Instance.SetCoin(coin);
    }
     */
}
