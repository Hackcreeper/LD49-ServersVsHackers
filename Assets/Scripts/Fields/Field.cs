using Towers;
using UnityEngine;

namespace Fields
{
    public abstract class Field : MonoBehaviour
    {
        public int row;
        public int column;
        
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
    }
}
