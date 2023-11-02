using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button MainMenuGameGameButton;
    [SerializeField] private Button resumeGameButton;
    [SerializeField] private Button optionGameButton;
    [SerializeField] private Button saveGameButton;
    [SerializeField] private Button loadGameButton;

    private void Awake()
    {
        MainMenuGameGameButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        resumeGameButton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.TogglePauseGame();
        });
        optionGameButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
        });
        saveGameButton.onClick.AddListener(() =>
        {
            Player.Instance.SavePlayer();
        });
        loadGameButton.onClick.AddListener(() =>
        {
            Player.Instance.LoadPlayer();
        });
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitchenGameManager.Instance.OnGameUnPause += KitchenGameManager_OnGameUnPause;
        Hide();
    }

    private void KitchenGameManager_OnGameUnPause(object sender, EventArgs e)
    {
        Hide();
    }

    private void KitchenGameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
