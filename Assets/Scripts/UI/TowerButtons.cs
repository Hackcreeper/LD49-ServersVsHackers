using UnityEngine;

namespace UI
{
    public class TowerButtons : MonoBehaviour
    {
        public static TowerButtons Instance { get; private set; } 
        
        public GameObject towerButtonPrefab;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Rerender();
        }

        public void Rerender()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            
            foreach (var tower in UnlockedTowers.Instance.GetAllTowers())
            {
                var button = Instantiate(
                    towerButtonPrefab,
                    transform
                );

                button.GetComponent<TowerButton>().tower = tower;
            }
        }
    }
}