using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedContainerChanged;
    }

    private void Player_OnSelectedContainerChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
#if ENABLE_DEBUG_LOG_CHECK_NULL
            if (_clearCounter != null)
            {
                _clearCounter.Interact(Player.Instance);
            }

             else
            {
                Debug.Log($"Null object: {_clearCounter}");
            }
#endif
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
