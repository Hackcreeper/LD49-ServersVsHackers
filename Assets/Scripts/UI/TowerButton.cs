using Towers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class TowerButton : MonoBehaviour
    {
        public GameObject towerPrefab;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnBuy);
        }

        private void OnBuy()
        {
            if (Tower.OnHand)
            {
                return;
            }
            
            Instantiate(towerPrefab);
        }
    }
}