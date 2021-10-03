using UnityEngine;

namespace Towers
{
    public class CoinMiner : Tower
    {
        public float cooldown = 2f;
        public int producing = 25;
        
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
            _timer = cooldown;
        }
    }
}