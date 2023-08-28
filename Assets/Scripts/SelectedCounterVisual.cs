using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter _clearCounter;
    [SerializeField] private GameObject visualGameObject;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedConterChanged;
    }

    private void Player_OnSelectedConterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == _clearCounter)
        {
            _clearCounter.Interact();
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        visualGameObject.SetActive(true);
    }
    private void Hide()
    {
        visualGameObject.SetActive(false);
    }
}
