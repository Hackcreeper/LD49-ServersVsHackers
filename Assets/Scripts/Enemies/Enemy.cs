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
        private bool _informedField;
        private bool _locked;

        public Field currentField;
        public Field targetField;
        
        #region UNITY
        
        private void Start()
        {
            TargetNextField();
        }
        
        protected virtual void Update()
        {
            if (_locked)
            {
                return;
            }
            
            if (MustStop())
            {
                transform.position = GetRealPositionOfField(currentField);
                _timer = 0;

                if (_attackTimer > 0f)
                {
                    _attackTimer -= Time.deltaTime;
                    return;
                }
                
                currentField.GetTower().TakeDamage();
                _attackTimer = attackCooldown;
                
                return;
            }
            
            _timer += Time.deltaTime / tileMoveSpeed;
            transform.position = Vector3.Lerp(
                _startPosition,
                GetRealPositionOfField(targetField),
                _timer
            );

            if (currentField.GetTower() && currentField.GetTower().walkable)
            {
                if (_timer > 0.4f && !_informedField)
                {
                    currentField.GetTower().OnWalkOver(this);
                    _informedField = true;
                }
            }
            
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
            _informedField = false;
            SetTarget(FieldGenerator.Instance.GetPreviousFieldInRow(row, currentField));
        }

        public void SetTargetWithStartPosition(Field target, Vector3 startPosition, float percentage)
        {
            _timer = percentage;
            _startPosition = startPosition;
            targetField = target;            
        }
        
        public void SetTarget(Field target)
        {
            _timer = 0f;
            _startPosition = transform.position;
            targetField = target;
        }

        public Vector3 GetRealPositionOfField(Field field)
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
        
        #region GENERAL

        protected bool MustStop()
        {
            return (currentField.GetTower() && !currentField.GetTower().walkable) || _locked;
        }

        public void Lock()
        {
            _locked = true;
        }

        public void Unlock()
        {
            _locked = false;
        }
        
        #endregion
    }
}