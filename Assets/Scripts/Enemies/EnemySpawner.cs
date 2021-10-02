using System.Collections.Generic;
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
        }
    }
}