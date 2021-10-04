using UnityEngine;

namespace Enemies
{
    public class Pacman : Enemy
    {
        public MeshRenderer meshRenderer;
        public Material[] randomMaterials;

        protected override void Start()
        {
            base.Start();

            meshRenderer.material = randomMaterials[Random.Range(0, randomMaterials.Length)];
        }

        protected override bool MustStop()
        {
            return false;   
        }
    }
}