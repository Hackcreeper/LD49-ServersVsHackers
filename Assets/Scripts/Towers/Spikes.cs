using Enemies;
using UnityEngine;

namespace Towers
{
    public class Spikes : Tower
    {
        public int damage = 2;
        
        protected override void OnPlace()
        {
            
        }

        protected override void OnUpdate()
        {
            
        }

        public override void OnWalkOver(Enemy enemy)
        {
            base.OnWalkOver(enemy);
            
            enemy.TakeDamage(damage);
        }
    }
}