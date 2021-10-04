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

        private List<EnemySpawnField> _spawnFields = new List<EnemySpawnField>();
        private Dictionary<int, List<Enemy>> _enemies = new Dictionary<int, List<Enemy>>();

        private void Awake()
        {
            Instance = this;
        }

        public void AddSpawnField(EnemySpawnField field)
        {
            _spawnFields.Add(field);
        }

        public void SpawnEnemy(GameObject prefab, int amount = 1)
        {
            for (var i = 0; i < amount; i++)
            {
                var possibleFields = _spawnFields.Where(field => !field.compromised).ToArray();
                
                var spawner = possibleFields[Random.Range(0, possibleFields.Length)];
                var position = spawner.transform.position;

                var enemy = Instantiate(
                    prefab,
                    new Vector3(
                        position.x + .5f,
                        position.y + .5f,
                        position.z
                    ),
                    Quaternion.identity
                );
                
                enemy.transform.SetParent(spawner.transform.parent);

                var component = enemy.GetComponent<Enemy>();
                component.row = spawner.row;
                component.currentField = spawner;

                if (!_enemies.ContainsKey(spawner.row))
                {
                    _enemies.Add(spawner.row, new List<Enemy>());
                }

                _enemies[spawner.row].Add(component);
            }
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

        public void Clean()
        {
            Instance = null;
        }

        public int GetTotalEnemies()
        {
            return _enemies.Values.Sum(list => list.Count);
        }
    }
}