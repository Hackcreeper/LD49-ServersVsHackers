using UnityEngine;

namespace Enemies
{
    public class Slime : LittleCritter
    {
        public GameObject lowerPrefab;

        protected override void Die()
        {
            if (lowerPrefab)
            {
                EnemySpawner.Instance.SpawnEnemyOnField(lowerPrefab, currentField);
                EnemySpawner.Instance.SpawnEnemyOnField(lowerPrefab, FieldGenerator.Instance.GetNextFieldInRow(row, currentField));
            }

            base.Die();
        }
    }
}