using System.Collections;
using Enemies;
using Fields;
using UnityEngine;

namespace Towers
{
    public class VPN : Tower
    {
        public Mesh defaultFieldMesh;
        public Mesh holeFieldMesh;
        public float suckSpeed = 1f;
        public float cooldown = 10f;
        public Material defaultMaterial;
        public Material cooldownMaterial;
        public MeshRenderer meshRenderer;

        private float _timer;
        
        protected override void OnPlace()
        {
            field.GetComponent<MeshFilter>().mesh = holeFieldMesh;
        }

        protected override void OnUpdate()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _timer = 0f;
                meshRenderer.material = defaultMaterial;
            }
        }

        public override void OnWalkOver(Enemy enemy)
        {
            base.OnWalkOver(enemy);

            if (_timer > 0f)
            {
                return;
            }

            _timer = cooldown;
            meshRenderer.material = cooldownMaterial;
            
            StartCoroutine(Suck(enemy));
        }

        private IEnumerator Suck(Enemy enemy)
        {
            // Lock enemy
            enemy.Lock();

            // Suck in the enemy (animation -> move down -> size down)
            var timer = 0f;
            var enemyTransform = enemy.transform;
            var originalScale = enemyTransform.localScale;
            var originalPosition = enemyTransform.position;

            while (timer < 1)
            {
                timer += Time.deltaTime / suckSpeed;

                enemyTransform.localScale = Vector3.Lerp(
                    originalScale,
                    originalScale * 0.4f,
                    timer
                );

                enemyTransform.position = Vector3.Lerp(
                    originalPosition,
                    new Vector3(originalPosition.x, originalPosition.y - 1f, originalPosition.z),
                    timer
                );

                yield return new WaitForEndOfFrame();
            }

            // Spawn new hole on the first row (use given mesh instance)
            var targetField = GetTargetField();
            targetField.GetComponent<MeshFilter>().mesh = holeFieldMesh;

            // Suck out the enemy (little jump maybe? size up again)
            enemyTransform.position = targetField.transform.position - new Vector3(0, 1f, 0);

            timer = 0f;
            originalPosition = enemyTransform.position;
            while (timer < 1)
            {
                timer += Time.deltaTime / suckSpeed;

                enemyTransform.localScale = Vector3.Lerp(
                    originalScale * .4f,
                    originalScale,
                    timer
                );

                enemyTransform.position = Vector3.Lerp(
                    originalPosition,
                    new Vector3(originalPosition.x, originalPosition.y + 2f, originalPosition.z),
                    timer
                );

                yield return new WaitForEndOfFrame();
            }

            // Remove hole (before enemy hits bottom)
            targetField.GetComponent<MeshFilter>().mesh = defaultFieldMesh;

            // Enemy should fall down
            timer = 0f;
            originalPosition = enemyTransform.position;
            while (timer < 1)
            {
                timer += Time.deltaTime / suckSpeed * 5f;

                enemyTransform.position = Vector3.Lerp(
                    originalPosition,
                    targetField.transform.position + new Vector3(0, .5f, 0),
                    timer
                );

                yield return new WaitForEndOfFrame();
            }

            // set fields:
            enemy.currentField = targetField;
            enemy.SetTargetWithStartPosition(
                FieldGenerator.Instance.GetPreviousFieldInRow(targetField.row, enemy.currentField),
                enemy.GetRealPositionOfField(enemy.currentField),
                0.5f
            );
            
            EnemySpawner.Instance.MoveEnemyInOtherRow(enemy, targetField.row);

            // Unlock enemy
            enemy.Unlock();
        }

        protected virtual Field GetTargetField()
        {
            return FieldGenerator.Instance.GetLastFieldInRow(row);
        }
        
        private void OnDestroy()
        {
            if (field)
            {
                field.GetComponent<MeshFilter>().mesh = defaultFieldMesh;
            }
        }
    }
}