using UnityEngine;

namespace Towers
{
    public class UsbSlot : Tower
    {
        public Tower pluggedTower;
        
        protected override void OnPlace()
        {
            
        }

        protected override void OnUpdate()
        {
            
        }

        public Tower GetTower()
        {
            if (pluggedTower)
            {
                return pluggedTower;
            }

            return this;
        }
    }
}