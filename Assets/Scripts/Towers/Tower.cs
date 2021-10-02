using Fields;
using UnityEngine;

namespace Towers
{
    public abstract class Tower : MonoBehaviour
    {
        public static Tower OnHand;
        
        public bool blueprint = true;
        public int row;
        public int column;
        public Field field;
        public int health = 3;
        public bool canTakeDamage = true;
        public TowerData data;

        private Camera _camera;

        #region UNITY

        protected virtual void Awake()
        {
            _camera = Camera.main;

            if (blueprint)
            {
                OnHand = this;
            }
        }
        
        private void Update()
        {
            if (blueprint)
            {
                UpdateBlueprint();
                return;
            }
            
            OnUpdate();
        }

        #endregion
        
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

        public void PlaceBlueprintAtField(Field f)
        {
            blueprint = false;
            AttachToField(f);
            
            row = f.row;
            column = f.column;
            field = f;

            f.tower = this;

            OnHand = null;
            
            OnPlace();
        }

        private void PlaceBlueprint()
        {
            if (!PlaceableField.ActiveField.CanPlace(this))
            {
                return;
            }
            
            blueprint = false;
            AttachToActiveField();
            
            row = PlaceableField.ActiveField.row;
            column = PlaceableField.ActiveField.column;
            field = PlaceableField.ActiveField;

            PlaceableField.ActiveField.PlaceTower(this);

            OnHand = null;
            OnPlace();
        }
        
        #endregion

        #region MOVEMENT
        
        private void AttachToMouse()
        {
            var mouseScreenPosition = Input.mousePosition;
            var mouseWorldPosition = _camera.ScreenToWorldPoint(new Vector3(
                mouseScreenPosition.x,
                mouseScreenPosition.y,
                _camera.nearClipPlane + 8
            ));
            
            transform.position = mouseWorldPosition;
        }

        private void AttachToActiveField()
        {
            AttachToField(PlaceableField.ActiveField);
        }

        private void AttachToField(Field f)
        {
            var position = f.transform.position;
            
            transform.position = new Vector3(
                position.x,
                position.y + 0.5f,
                position.z
            );
        }
    
        #endregion
        
        #region ABSTRACT

        protected abstract void OnPlace();
        protected abstract void OnUpdate();

        #endregion
        
        #region HEALTH

        public void TakeDamage(int damage = 1)
        {
            if (!canTakeDamage)
            {
                return;
            }
            
            health -= damage;
            if (health > 0)
            {
                return;
            }

            field.tower = null;
            Destroy(gameObject);
        }
        
        #endregion
    }
}