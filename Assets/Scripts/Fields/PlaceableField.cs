using Towers;
using UnityEngine;

namespace Fields
{
    public class PlaceableField : Field
    {
        public static PlaceableField ActiveField { get; private set; }
        
        public Material defaultMaterial;
        public Material hoverErrorMaterial;
        public Material hoverOkMaterial;
        
        private void OnMouseEnter()
        {
            if (Tower.OnHand)
            {
                if (!compromised)
                {
                    MeshRenderer.material = CanPlace(Tower.OnHand) ? hoverOkMaterial : hoverErrorMaterial;   
                }
            }

            ActiveField = this;
        }

        private void OnMouseExit()
        {
            if (compromised)
            {
                return;
            }
            
            MeshRenderer.material = defaultMaterial;
            if (ActiveField == this)
            {
                ActiveField = null;
            }
        }

        public void PlaceTower(Tower t)
        {
            Coins.Instance.amount -= t.data.price;

            if (tower && tower is UsbSlot slot)
            {
                slot.pluggedTower = t;
            }
            else
            {
                tower = t;
            }
            
            MeshRenderer.material = defaultMaterial;
        }

        public bool CanPlace(Tower other)
        {
            if (compromised)
            {
                return false;
            }
            
            if (Coins.Instance.amount < other.data.price)
            {
                return false;
            }

            if (tower == null)
            {
                return !other.requiresUsb;
            }

            if (tower is UsbSlot slot)
            {
                if (slot.pluggedTower)
                {
                    return false;
                }
                
                return other.requiresUsb;
            }

            return false;
        }
    }
}