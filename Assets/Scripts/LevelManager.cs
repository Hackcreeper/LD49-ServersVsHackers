using System.Collections;
using Enemies;
using Towers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    
    private int? _sceneToBeLoaded = null;
    private int currentLevel;
    private Vector2 startPosition = Vector2.zero;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
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
        if (SceneManager.GetActiveScene().name != "Game")
        {
            return;
        }

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
            _sceneToBeLoaded = null;
            return;
        }
        
        // Otherwise just load the first level
        LoadLevel(1);
    }

    private void CleanSingletons()
    {
        FieldGenerator.Instance?.Clean();
        EnemySpawner.Instance?.Clean();

        if (Tower.OnHand)
        {
            Destroy(Tower.OnHand.gameObject);
        }
    }

    public void LoadLevel(int levelId, bool restarting = false)
    {
        UnlockedTowers.Instance.Unlock(levelId);
        
        if (levelId > 1 && !restarting)
        {
            startPosition += new Vector2(45, 45);
        }
        
        currentLevel = levelId;
        
        CleanSingletons();
        if (restarting)
        {
            RemoveAllLevels();
        }
        
        SceneManager.LoadSceneAsync($"Level{levelId}", LoadSceneMode.Additive);

        StartCoroutine(RemoveOldLevels());
    }

    private IEnumerator RemoveOldLevels()
    {
        yield return new WaitForSeconds(2);

        for (var i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.name.Contains("Level") && scene.name != $"Level{currentLevel}")
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }

    private void RemoveAllLevels()
    {
        for (var i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.name.Contains("Level"))
            {
                SceneManager.UnloadSceneAsync(scene);
            }
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
        LoadLevel(currentLevel, true);
    }

    public int GetCurrentLevel() => currentLevel;

    public Vector2 GetStartPosition() => startPosition;

    public void StartGameInLevel(int level)
    {
        startPosition = Vector2.zero;
        _sceneToBeLoaded = level;

        SceneManager.LoadScene("Game");
    }
}