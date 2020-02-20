using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class Loader
{
    public enum Scene
    {
        ClassicScene,
        LoadingScene,

    }

    private static Action loaderCallbackAction;

    // For loading the scene
    public static void Load(Scene scene)
    {
        // loaderCallbackAction will trigger after the loading screen is loaded
        loaderCallbackAction = () =>
        {
            // Load specific scene after Loading screen
            SceneManager.LoadScene(scene.ToString());
        };

        // Load loading screen
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }
    
    public static void LoaderCallback()
    {
        if(loaderCallbackAction != null)
        {
            loaderCallbackAction();
            loaderCallbackAction = null;
        }
    }
}
