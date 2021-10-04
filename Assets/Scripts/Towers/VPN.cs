using System;
using UnityEngine;

namespace Towers
{
    public class VPN : Tower
    {
        public Mesh defaultFieldMesh;
        public Mesh holeFieldMesh;
        
        protected override void OnPlace()
        {
            field.GetComponent<MeshFilter>().mesh = holeFieldMesh;
        }

        protected override void OnUpdate()
        {
            
        }

        private void OnDestroy()
        {
            if (field)
            {
                field.GetComponent<MeshFilter>().mesh = defaultFieldMesh;
            }
        }
    }
}