using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public static Scene targetScene;

    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene
    }
    
    public static void Load(Scene targetSceneName)
    {
        Loader.targetScene = targetSceneName;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
