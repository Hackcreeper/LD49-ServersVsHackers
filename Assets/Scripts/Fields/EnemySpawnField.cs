using Enemies;

namespace Fields
{
    public class EnemySpawnField : Field
    {
        private void Start()
        {
            EnemySpawner.Instance.AddSpawnField(this);
        }

        public override void OnProjectileEnter(Projectile projectile)
        {
            base.OnProjectileEnter(projectile);
            
            Destroy(projectile.gameObject);
        }
    }
}