using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    
    private int? _sceneToBeLoaded = null;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Probably added the level manually via editor
        if (SceneManager.sceneCount > 1)
        {
            return;
        }
        
        // If there is specific level given, which should be loaded, load it
        if (_sceneToBeLoaded.HasValue)
        {
            LoadLevel(_sceneToBeLoaded.Value);
            return;
        }
        
        // Otherwise just load the first level
        LoadLevel(1);
    }

    private void CleanSingletons()
    {
        FieldGenerator.Instance?.Clean();
    }

    public void LoadLevel(int levelId)
    {
        var somethingUnloaded = false;
        
        for (var i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.name.Contains("Level"))
            {
                somethingUnloaded = true;
                SceneManager.UnloadSceneAsync(scene).completed += delegate(AsyncOperation operation)
                {
                    CleanSingletons();
                    SceneManager.LoadSceneAsync($"Level{levelId}", LoadSceneMode.Additive);
                };
            }
        }

        if (!somethingUnloaded)
        {
            CleanSingletons();
            SceneManager.LoadSceneAsync($"Level{levelId}", LoadSceneMode.Additive);
        }
    }
}