using Enemies;
using Towers;
using UnityEngine;

namespace Fields
{
    public abstract class Field : MonoBehaviour
    {
        public int row;
        public int column;
        public Tower tower;

        protected MeshRenderer MeshRenderer;

        private void Awake()
        {
            MeshRenderer = GetComponent<MeshRenderer>();
        }

        public void SetCoords(int x, int y)
        {
            this.row = y;
            this.column = x;
        }

        public virtual void OnEnemyEnter(Enemy enemy)
        {
        }
        
        public virtual void OnProjectileEnter(Projectile projectile)
        {
        }

        public Tower GetTower()
        {
            if (tower is UsbSlot slot)
            {
                return slot.GetTower();
            }
            
            return tower;
        }
    }
}