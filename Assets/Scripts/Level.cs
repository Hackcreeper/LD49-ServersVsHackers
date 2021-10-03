using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int startCoins;
    public PlannedEnemy[] plannedEnemies;

    private List<PlannedEnemy> _planned = new List<PlannedEnemy>();
    private int _lastSpawned = -1;
    private float _timer = 0;

    private void Start()
    {
        _planned.AddRange(plannedEnemies);
        _planned.Sort((a, b) => a.time - b.time);

        Coins.Instance.amount = startCoins;
    }

    private void Update()
    {
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
}