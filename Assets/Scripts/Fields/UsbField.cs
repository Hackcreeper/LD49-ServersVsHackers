using Towers;
using UnityEngine;

namespace Fields
{
    public class UsbField : PlaceableField
    {
        public GameObject usbPrefab;
        
        private void Start()
        {
            Instantiate(usbPrefab).GetComponent<UsbSlot>().PlaceBlueprintAtField(this);
        }
    }
}