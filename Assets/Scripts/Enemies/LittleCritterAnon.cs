using UnityEngine;

namespace Enemies
{
    public class LittleCritterAnon : LittleCritter
    {
        public GameObject mask;
        
        public override void TakeDamage(int damage = 1)
        {
            base.TakeDamage(damage);

            if (health <= 3)
            {
                mask.SetActive(false);
            }
        }
    }
}