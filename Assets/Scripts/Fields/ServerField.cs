using System;
using Enemies;
using Towers;
using UnityEngine;

namespace Fields
{
    public class ServerField : Field
    {
        public GameObject serverPrefab;

        private Server _server;

        private void Start()
        {
            var server = Instantiate(serverPrefab);

            _server = server.GetComponent<Server>();
            _server.PlaceBlueprintAtField(this);
        }

        public override void OnEnemyEnter(Enemy enemy)
        {
            base.OnEnemyEnter(enemy);
            
            _server.Corrupt();
            Destroy(enemy.gameObject);
        }
    }
}