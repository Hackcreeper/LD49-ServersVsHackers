using System;
using Enemies;
using Fields;
using UI;
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
        public bool requiresUsb;
        public bool walkable;
        public AudioClip dieSound;

        private Camera _camera;
        private bool _onCurrentLevel = true;

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
            if (!_onCurrentLevel)
            {
                return;
            }
            
            if (blueprint)
            {
                UpdateBlueprint();
                return;
            }
            
            OnUpdate();
        }

        protected virtual void OnDestroy()
        {
            if (dieSound != null)
            {
                Audio.Instance?.Play(dieSound);
            }
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
            
            transform.SetParent(f.transform);
        }

        private void PlaceBlueprint()
        {
            if (!PlaceableField.ActiveField.CanPlace(this))
            {
                return;
            }

            var tutorial = FindObjectOfType<Tutorial>();
            if (tutorial)
            {
                tutorial.DisableTutorial();
            }

            blueprint = false;
            AttachToActiveField();
            
            row = PlaceableField.ActiveField.row;
            column = PlaceableField.ActiveField.column;
            field = PlaceableField.ActiveField;

            PlaceableField.ActiveField.PlaceTower(this);

            OnHand = null;
            OnPlace();
            
            transform.SetParent(PlaceableField.ActiveField.transform);
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

            var offset = (f.GetTower() is UsbSlot && this.requiresUsb) ? 0.15f : 0.5f;
            
            transform.position = new Vector3(
                position.x,
                position.y + offset,
                position.z
            );
        }
    
        #endregion
        
        #region ABSTRACT

        protected abstract void OnPlace();
        protected abstract void OnUpdate();

        public virtual void OnWalkOver(Enemy enemy)
        {
        }

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
            
            Destroy(gameObject);
        }
        
        #endregion
        
        #region GENERAL

        public void NewLevelLoaded()
        {
            _onCurrentLevel = false;
        }
        
        #endregion
    }
}