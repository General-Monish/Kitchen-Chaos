using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoaderUI 
{
   public enum Scene
    {
        MainMenu,
        Game,
        LoadingScene,
    }

    public static Scene targetScene;

    public static void load(Scene targetScene)
    {
        LoaderUI.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
        
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
