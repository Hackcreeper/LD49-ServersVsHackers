using System.Collections;
using System.Collections.Generic;
using Enemies;
using UI;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int startCoins;
    public PlannedEnemy[] plannedEnemies;

    private List<PlannedEnemy> _planned = new List<PlannedEnemy>();
    private int _lastSpawned = -1;
    private float _timer = 0;
    private LevelState _state = LevelState.Started;

    private void Start()
    {
        _planned.AddRange(plannedEnemies);
        _planned.Sort((a, b) => a.time - b.time);

        Coins.Instance.amount = startCoins;

        StartCoroutine(ShowIntro());
    }

    private void Update()
    {
        if (_state != LevelState.Running)
        {
            return;
        }
        
        if (_lastSpawned + 1 >= _planned.Count)
        {
            CheckForWin();
            
            return;
        }
        
        _timer += Time.deltaTime;

        if (_timer > _planned[_lastSpawned + 1].time)
        {
            EnemySpawner.Instance.SpawnEnemy(_planned[_lastSpawned + 1].enemyPrefab, _planned[_lastSpawned + 1].amount);
            _lastSpawned++;
        }
    }

    private void CheckForWin()
    {
        if (EnemySpawner.Instance.GetTotalEnemies() > 0)
        {
            return;
        }
        
        LevelManager.Instance.LoadNextLevel();
    }

    private IEnumerator ShowIntro()
    {
        Banner.Instance.ShowIntroBanner();

        yield return new WaitForSeconds(2);

        _state = LevelState.Running;
        
        Banner.Instance.HideIntroBanner();
    }

    private enum LevelState
    {
        Started,
        Running,
        Finished,
        Lost
    }
}