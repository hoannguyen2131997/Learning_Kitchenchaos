using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerUI : MonoBehaviour
{
    public static GameControllerUI Instance { get; private set; }
    [SerializeField] private Button OKBtn;
    [SerializeField] private Button CancelBtn;

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    
    
    private void Awake()
    {
        Instance = this;
        
        OKBtn.onClick.AddListener(() =>
        {
            Hide();
        });
        
        CancelBtn.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        Hide();
        
    }

    private void GetComponentChildButtons()
    {
        
    }

    private void UpdateKeyBoardText()
    {
        
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
