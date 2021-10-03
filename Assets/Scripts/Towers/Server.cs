using UnityEngine;

namespace Towers
{
    public class Server : Tower
    {
        public Material corruptedMaterial;
        public bool corrupted;
        public MeshRenderer errorMeshRenderer;
        public int lightMaterialId;

        protected override void Awake()
        {
            base.Awake();
        }

        public void Corrupt()
        {
            corrupted = true;

            var materials = errorMeshRenderer.materials;
            materials[lightMaterialId] = corruptedMaterial;
            errorMeshRenderer.materials = materials;
            
            // foreach (var meshRenderer in _meshRenderers)
            // {
            //     meshRenderer.material = corruptedMaterial;
            // }

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