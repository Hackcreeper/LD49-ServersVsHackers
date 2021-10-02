using Enemies;
using UnityEngine;

namespace Towers
{
    public class AntiVirus : Tower
    {
        public float cooldown = 2;
        public GameObject projectilePrefab;

        private float _timer;
        
        protected override void OnPlace()
        {
            _timer = cooldown;
        }

        protected override void OnUpdate()
        {
            _timer -= Time.deltaTime;
            if (_timer > 0)
            {
                return;
            }

            if (!EnemySpawner.Instance.HasEnemyAfter(row, column))
            {
                _timer = 0f;
                return;
            }
            
            _timer = cooldown;

            var projectile = Instantiate(
                projectilePrefab,
                transform.position,
                Quaternion.identity
            );

            var component = projectile.GetComponent<Projectile>();
            component.row = row;
            component.currentField = field;
        }
    }
}