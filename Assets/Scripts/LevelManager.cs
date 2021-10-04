using System;
using Enemies;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    
    private int? _sceneToBeLoaded = null;
    private int currentLevel;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += (old, current) =>
        {
            if (current.name == "Game")
            {
                Start();
            }
        };
    }

    private void Start()
    {
        // Probably added the level manually via editor
        if (SceneManager.sceneCount > 1)
        {
            currentLevel = int.Parse(SceneManager.GetSceneAt(1).name.Replace("Level", ""));
            
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
        EnemySpawner.Instance?.Clean();
    }

    public void LoadLevel(int levelId)
    {
        currentLevel = levelId;
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

    public void LoadNextLevel()
    {
        var nextLevel = currentLevel + 1;
        
        if (!Application.CanStreamedLevelBeLoaded($"Level{nextLevel}"))
        {
            Debug.Log("Finished the game");
            return;
        }

        LoadLevel(nextLevel);
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevel);
    }

    public int GetCurrentLevel() => currentLevel;
}