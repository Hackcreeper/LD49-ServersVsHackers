using UnityEngine;

namespace Towers
{
    public class CoinMiner : Tower
    {
        public float cooldown = 6f;
        public int producing = 20;
        public int particleEmit = 4;
        public ParticleSystem particles;

        private float _timer = 0f;
        
        protected override void OnPlace()
        {
            _timer = cooldown;
        }

        protected override void OnUpdate()
        {
            _timer -= Time.deltaTime;
            if (_timer > 0f)
            {
                return;
            }

            Coins.Instance.amount += producing;
            particles.Emit(particleEmit);
            _timer = cooldown;
        }
    }
}