using UnityEngine;

namespace Enemies
{
    public class LittleCritter : Enemy
    {
        public Animator animator;

        protected override void Update()
        {
            animator?.SetBool("walking", currentField.GetTower() == null);
            
            base.Update();
        }
    }
}