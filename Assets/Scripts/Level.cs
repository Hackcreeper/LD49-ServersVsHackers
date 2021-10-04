using System.Collections.Generic;
using Enemies;
using Towers;
using UI;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int startCoins;
    public PlannedEnemy[] plannedEnemies;
    public TowerData unlock;
    public int requiredServersAlive = 3;

    private List<PlannedEnemy> _planned = new List<PlannedEnemy>();
    private int _lastSpawned = -1;
    private float _timer = 0;
    private LevelState _state = LevelState.Running;

    private void Start()
    {
        var startPosition = LevelManager.Instance.GetStartPosition();
        transform.position = new Vector3(
            startPosition.x,
            0,
            startPosition.y
        );

        CameraMover.Instance.MoveTo(new Vector3(
            startPosition.x,
            5.42f,
            startPosition.y - 6.35f
        ));

        TowerButtons.Instance.Rerender();

        _planned.AddRange(plannedEnemies);
        _planned.Sort((a, b) => a.time - b.time);

        Coins.Instance.amount = startCoins;

        Banner.Instance.UpdateLevelName();
    }

    private void Update()
    {
        if (_state != LevelState.Running)
        {
            return;
        }

        if (FieldGenerator.Instance.AliveServers() < requiredServersAlive)
        {
            EndGame();
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

    private void EndGame()
    {
        _state = LevelState.Failed;
        GameOver.Instance.Show();
    }

    private void CheckForWin()
    {
        if (EnemySpawner.Instance.GetTotalEnemies() > 0)
        {
            return;
        }

        _state = LevelState.Finished;

        Congratulations.Instance.Show(unlock);
        UnlockedTowers.Instance.AddTower(unlock);
    }
    
    private enum LevelState
    {
        Running,
        Finished,
        Failed
    }
}