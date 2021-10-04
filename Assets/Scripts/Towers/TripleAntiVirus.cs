using UnityEngine;

namespace Towers
{
    public class TripleAntiVirus : DoubleAntiVirus
    {
        public Transform projectileSpawn3;
        
        protected override void Shoot()
        {
            base.Shoot();
            
            var projectile = Instantiate(
                projectilePrefab,
                projectileSpawn3.position,
                Quaternion.identity
            );
            
            projectile.transform.SetParent(field.transform.parent);
        }
    }
}