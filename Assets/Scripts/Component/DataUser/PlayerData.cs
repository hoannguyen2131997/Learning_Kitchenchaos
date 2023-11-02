using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int coins;
    public float timer;

    public PlayerData(Player player)
    {
        coins = player.coins;
        timer = player.timer;
    }
}