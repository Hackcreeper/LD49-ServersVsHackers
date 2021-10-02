using Fields;
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

        protected MeshRenderer[] MeshRenderers;
        
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            MeshRenderers = GetComponentsInChildren<MeshRenderer>();

            if (blueprint)
            {
                OnHand = this;
            }
        }

        private void Start()
        {
            if (!blueprint)
            {
                return;
            }
            
            foreach (var meshRenderer in MeshRenderers)
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
            if (!PlaceableField.ActiveField)
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

        public void PlaceBlueprintAtField(Field field)
        {
            blueprint = false;
            AttachToField(field);
            
            foreach (var meshRenderer in MeshRenderers)
            {
                meshRenderer.material = defaultMaterial;
            }

            row = field.row;
            column = field.column;

            field.tower = this;

            OnHand = null;
        }

        private void PlaceBlueprint()
        {
            if (!PlaceableField.ActiveField.CanPlace(this))
            {
                return;
            }
            
            blueprint = false;
            AttachToActiveField();
            
            foreach (var meshRenderer in MeshRenderers)
            {
                meshRenderer.material = defaultMaterial;
            }

            row = PlaceableField.ActiveField.row;
            column = PlaceableField.ActiveField.column;
            
            PlaceableField.ActiveField.PlaceTower(this);

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
            AttachToField(PlaceableField.ActiveField);
        }

        private void AttachToField(Field field)
        {
            var position = field.transform.position;
            
            transform.position = new Vector3(
                position.x,
                position.y + 0.5f,
                position.z
            );
        }
    
        #endregion
    }
}