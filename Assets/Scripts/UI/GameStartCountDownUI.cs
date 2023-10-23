using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";
    [SerializeField] private TextMeshProUGUI textCountDown;

    private Animator _animator;
    private int previousCountDownNumber;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }

    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {

        int countDownNumber = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountdownToStartTimer());
        textCountDown.text = countDownNumber.ToString();
        
        if (previousCountDownNumber != countDownNumber)
        {
            previousCountDownNumber = countDownNumber;
            _animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountDownSound();
            
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
