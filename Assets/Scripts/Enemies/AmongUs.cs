using UnityEngine;

namespace Enemies
{
    public class AmongUs : Enemy
    {
        public Animator animator;
        private static readonly int Walking = Animator.StringToHash("walking");

        protected override void Update()
        {
            animator?.SetBool(Walking, !MustStop());
            
            base.Update();
        }
    }
}