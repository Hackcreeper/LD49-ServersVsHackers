using Fields;
using UnityEngine;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        public float tileMoveSpeed = 2;
        public float attackCooldown = 1;
        public int row;
        public int health = 3;

        private float _timer;
        private float _attackTimer;
        private Vector3 _startPosition;

        public Field currentField;
        public Field targetField;
        
        #region UNITY
        
        private void Start()
        {
            TargetNextField();
        }

        private void Update()
        {
            if (currentField.tower)
            {
                transform.position = GetRealPositionOfField(currentField);
                _timer = 0;

                if (_attackTimer > 0f)
                {
                    _attackTimer -= Time.deltaTime;
                    return;
                }
                
                currentField.tower.TakeDamage();
                _attackTimer = attackCooldown;
                
                return;
            }
            
            _timer += Time.deltaTime / tileMoveSpeed;
            transform.position = Vector3.Lerp(
                _startPosition,
                GetRealPositionOfField(targetField),
                _timer
            );

            if (_timer >= 1f)
            {
                currentField = targetField;
                TargetNextField();
                
                currentField.OnEnemyEnter(this);
            }
        }

        #endregion
        
        #region MOVEMENT
        
        private void TargetNextField()
        {
            SetTarget(FieldGenerator.Instance.GetPreviousFieldInRow(row, currentField));
        }

        private void SetTarget(Field target)
        {
            _timer = 0f;
            _startPosition = transform.position;
            targetField = target;
        }

        private Vector3 GetRealPositionOfField(Field field)
        {
            var position = field.transform.position;

            return new Vector3(
                position.x + .5f,
                position.y + .5f,
                position.z
            );
        }

        #endregion
        
        #region HEALTH
        
        public virtual void TakeDamage(int damage = 1)
        {
            health -= damage;
            if (health > 0)
            {
                return;
            }
            
            EnemySpawner.Instance.KillEnemy(this);
        }
        
        #endregion
    }
}