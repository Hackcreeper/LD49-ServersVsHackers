using UnityEngine;

namespace Towers
{
    public class Server : Tower
    {
        public Material corruptedMaterial;
        public bool corrupted;

        public void Corrupt()
        {
            corrupted = true;
            
            foreach (var meshRenderer in MeshRenderers)
            {
                meshRenderer.material = corruptedMaterial;
            }
        }
    }
}