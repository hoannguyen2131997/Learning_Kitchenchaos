using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button controllerButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;
    public static OptionsUI Instance;
    private void Awake()
    {
        Instance = this;
        Hide();
        soundEffectButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeSoundVolume();
            
            UpdateVisual();
        });
        
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeMusicVolume();
            UpdateVisual();
        });
        
        quitButton.onClick.AddListener(() =>
        {
            Hide();
        });
        
        controllerButton.onClick.AddListener(() =>
        {
            GameControllerUI.Instance.Show();
        });
    }

    private void UpdateVisual()
    {
        soundEffectText.text = $"Sound Effect: {Mathf.Round(SoundManager.Instance.GetVolumeSound() * 10f)}";
        musicText.text = $"Music: {Mathf.Round(MusicManager.Instance.GetMusicVolume() * 10f)}";
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        UpdateVisual();
    }
}
