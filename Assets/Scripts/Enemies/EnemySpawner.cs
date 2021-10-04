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
                SpawnEnemyOnField(prefab, spawner);
            }
        }

        public void SpawnEnemyOnField(GameObject prefab, Field field)
        {
            var position = field.transform.position;

            var enemy = Instantiate(
                prefab,
                new Vector3(
                    position.x + .5f,
                    position.y + .5f,
                    position.z
                ),
                Quaternion.identity
            );
                
            enemy.transform.SetParent(field.transform.parent);

            var component = enemy.GetComponent<Enemy>();
            component.row = field.row;
            component.currentField = field;

            if (!_enemies.ContainsKey(field.row))
            {
                _enemies.Add(field.row, new List<Enemy>());
            }

            _enemies[field.row].Add(component);
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

        public void MoveEnemyInOtherRow(Enemy enemy, int newRow)
        {
            _enemies[enemy.row].Remove(enemy);
            
            if (!_enemies.ContainsKey(newRow))
            {
                _enemies.Add(newRow, new List<Enemy>());
            }
            
            _enemies[newRow].Add(enemy);

            enemy.row = newRow;
        }
    }
}