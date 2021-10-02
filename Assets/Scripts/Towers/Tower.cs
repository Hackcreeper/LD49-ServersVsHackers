using System;
using UnityEngine;

namespace Towers
{
    public abstract class Tower : MonoBehaviour
    {
        public static Tower OnHand;
        
        public bool blueprint = true;
        public Material defaultMaterial;
        public Material blueprintMaterial;
        public int row;
        public int column;

        private Camera _camera;
        private MeshRenderer[] _meshRenderers;

        private void Awake()
        {
            _camera = Camera.main;
            _meshRenderers = GetComponentsInChildren<MeshRenderer>();

            OnHand = this;
        }

        private void Start()
        {
            foreach (var meshRenderer in _meshRenderers)
            {
                meshRenderer.material = blueprintMaterial;
            }
        }

        private void Update()
        {
            if (blueprint)
            {
                UpdateBlueprint();
                return;
            }
        }
        
        #region BLUEPRINT
        
        private void UpdateBlueprint()
        {
            if (!Field.ActiveField)
            {
                AttachToMouse();
                
                return;
            }
         
            if (Input.GetMouseButtonDown(0))
            {
                PlaceBlueprint();
                return;
            }
            
            AttachToActiveField();
        }

        private void PlaceBlueprint()
        {
            if (!Field.ActiveField.CanPlace(this))
            {
                return;
            }
            
            blueprint = false;
            AttachToActiveField();
            
            foreach (var meshRenderer in _meshRenderers)
            {
                meshRenderer.material = defaultMaterial;
            }

            row = Field.ActiveField.row;
            column = Field.ActiveField.column;
            
            Field.ActiveField.PlaceTower(this);

            OnHand = null;
        }
        
        #endregion

        #region MOVEMENT
        
        private void AttachToMouse()
        {
            var mouseScreenPosition = Input.mousePosition;
            var mouseWorldPosition = _camera.ScreenToWorldPoint(new Vector3(
                mouseScreenPosition.x,
                mouseScreenPosition.y,
                _camera.nearClipPlane + 5
            ));
            
            transform.position = mouseWorldPosition;
        }

        private void AttachToActiveField()
        {
            var position = Field.ActiveField.transform.position;
            
            transform.position = new Vector3(
                position.x,
                position.y + 0.5f,
                position.z
            );
        }
    
        #endregion
    }
}