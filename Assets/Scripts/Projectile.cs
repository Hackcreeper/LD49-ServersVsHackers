using Enemies;
using Fields;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float tileMoveSpeed = 1.2f;
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
        var enemies = EnemySpawner.Instance.GetEnemiesOnPosition(row, targetField.column);
        if (enemies.Length > 0)
        {
            foreach (var enemy in enemies)
            {
                enemy.TakeDamage();
            }
            
            Destroy(gameObject);
            
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
            
            currentField.OnProjectileEnter(this);
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