using TMPro;
using Towers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class TowerButton : MonoBehaviour
    {
        public TowerData tower;
        public TextMeshProUGUI title;
        public TextMeshProUGUI costs;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnBuy);

            title.text = tower.title;
            costs.text = tower.price.ToString();
        }
        
        private void OnBuy()
        {
            if (Tower.OnHand)
            {
                return;
            }
            
            var instance = Instantiate(tower.prefab);
            instance.GetComponent<Tower>().data = tower;
        }
    }
}