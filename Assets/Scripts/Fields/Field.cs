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
        public bool compromised;
        public Material compromisedMaterial;

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

        public void Compromise()
        {
            GetComponent<MeshRenderer>().material = compromisedMaterial;
            compromised = true;

            if (!tower)
            {
                return;
            }

            if (tower is Server)
            {
                return;
            }

            if (tower is UsbSlot slot)
            {
                if (slot.pluggedTower)
                {
                    Destroy(slot.pluggedTower.gameObject);
                }
            }

            Destroy(tower.gameObject);
        }
    }
}