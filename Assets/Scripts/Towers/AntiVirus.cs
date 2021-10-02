using Enemies;
using UnityEngine;

namespace Towers
{
    public class AntiVirus : Tower
    {
        public float cooldown = 2;
        public GameObject projectilePrefab;

        private float _timer;
        private bool _active;
        private Animator _animator;
        private static readonly int Active = Animator.StringToHash("active");

        protected override void Awake()
        {
            base.Awake();

            _animator = GetComponent<Animator>();
        }

        protected override void OnPlace()
        {
            _timer = cooldown;
        }

        protected override void OnUpdate()
        {
            var hasEnemy = EnemySpawner.Instance.HasEnemyAfter(row, column);
            if (hasEnemy && !_active)
            {
                _active = true;
                _animator.SetBool(Active, true);
                _timer = cooldown;
            }
            else if (!hasEnemy && _active)
            {
                _active = false;
                _animator.SetBool(Active, false);
            }
            
            _timer -= Time.deltaTime;
            if (_timer > 0)
            {
                return;
            }

            if (!hasEnemy)
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