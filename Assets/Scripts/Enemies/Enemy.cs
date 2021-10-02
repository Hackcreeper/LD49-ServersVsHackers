using System;
using Fields;
using UnityEngine;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        public float tileMoveSpeed = 2;
        public int row;

        private float _timer;
        private Vector3 _startPosition;

        public Field currentField;
        public Field targetField;

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

        private void TargetNextField()
        {
            SetTarget(FieldGenerator.Instance.GetNextFieldInRow(row, currentField));
        }

        public void SetTarget(Field target)
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
    }
}