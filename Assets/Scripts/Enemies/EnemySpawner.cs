using System;
using System.Collections.Generic;
using System.Linq;
using Fields;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner Instance { get; private set; } 
        
        public GameObject enemyPrefab;
        public float cooldown = 3;

        private List<EnemySpawnField> _spawnFields = new List<EnemySpawnField>();
        private float _timer;
        private Dictionary<int, List<Enemy>> _enemies = new Dictionary<int, List<Enemy>>();

        private void Awake()
        {
            Instance = this;
            _timer = cooldown;
        }

        public void AddSpawnField(EnemySpawnField field)
        {
            _spawnFields.Add(field);
        }
        
        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer > 0)
            {
                return;
            }

            SpawnEnemy();
            _timer = cooldown;
        }

        private void SpawnEnemy()
        {
            var spawner = _spawnFields[Random.Range(0, _spawnFields.Count)];
            var position = spawner.transform.position;
            
            var enemy = Instantiate(
                enemyPrefab,
                new Vector3(
                    position.x + .5f,
                    position.y + .5f,
                    position.z
                ),
                Quaternion.identity
            );

            var component = enemy.GetComponent<Enemy>();
            component.row = spawner.row;
            component.currentField = spawner;

            if (!_enemies.ContainsKey(spawner.row))
            {
                _enemies.Add(spawner.row, new List<Enemy>());
            }
            
            _enemies[spawner.row].Add(component);
        }

        public void KillEnemy(Enemy enemy)
        {
            _enemies[enemy.row].Remove(enemy);
            Destroy(enemy.gameObject);
        }

        public bool HasEnemyAfter(int row, int column)
        {
            if (!_enemies.ContainsKey(row))
            {
                return false;
            }

            return _enemies[row].Count(enemy => enemy.currentField.column >= column) > 0;
        }

        public Enemy[] GetEnemiesOnPosition(int row, int column)
        {
            if (!_enemies.ContainsKey(row))
            {
                return Array.Empty<Enemy>();
            }

            return _enemies[row].Where(enemy => enemy.currentField.column == column).ToArray();
        }
    }
}