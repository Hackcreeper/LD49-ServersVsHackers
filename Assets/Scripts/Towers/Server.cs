using UnityEngine;

namespace Towers
{
    public class Server : Tower
    {
        public Material corruptedMaterial;
        public bool corrupted;

        private MeshRenderer[] _meshRenderers;

        protected override void Awake()
        {
            base.Awake();
            
            _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        }

        public void Corrupt()
        {
            corrupted = true;
            
            foreach (var meshRenderer in _meshRenderers)
            {
                meshRenderer.material = corruptedMaterial;
            }

            FieldGenerator.Instance.CompromiseRow(row);
        }

        protected override void OnPlace()
        {
            
        }

        protected override void OnUpdate()
        {
            
        }
    }
}