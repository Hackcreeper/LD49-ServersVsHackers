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
        
        public Tower tower;
        
        private void OnMouseEnter()
        {
            if (Tower.OnHand)
            {
                MeshRenderer.material = CanPlace(Tower.OnHand) ? hoverOkMaterial : hoverErrorMaterial;   
            }

            ActiveField = this;
        }

        private void OnMouseExit()
        {
            MeshRenderer.material = defaultMaterial;
            if (ActiveField == this)
            {
                ActiveField = null;
            }
        }

        public void PlaceTower(Tower t)
        {
            tower = t;
            MeshRenderer.material = defaultMaterial;
        }

        public bool CanPlace(Tower other)
        {
            return tower == null;
        }
    }
}