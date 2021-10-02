using Enemies;

namespace Fields
{
    public class EnemySpawnField : Field
    {
        private void Start()
        {
            EnemySpawner.Instance.AddSpawnField(this);
        }
    }
}