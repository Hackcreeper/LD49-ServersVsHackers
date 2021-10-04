using UnityEngine;

namespace Towers
{
    public class TripleAntiVirus : DoubleAntiVirus
    {
        public Transform projectileSpawn3;
        
        protected override void Shoot()
        {
            base.Shoot();
            
            Instantiate(
                projectilePrefab,
                projectileSpawn3.position,
                Quaternion.identity
            );
        }
    }
}