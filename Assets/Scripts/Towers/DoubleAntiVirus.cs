using UnityEngine;

namespace Towers
{
    public class DoubleAntiVirus : AntiVirus
    {
        public Transform projectileSpawn2;

        protected override void Shoot()
        {
            base.Shoot();
            
            Instantiate(
                projectilePrefab,
                projectileSpawn2.position,
                Quaternion.identity
            );
        }
    }
}