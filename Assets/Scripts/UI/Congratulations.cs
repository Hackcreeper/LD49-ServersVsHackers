using TMPro;
using Towers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Congratulations : MonoBehaviour
    {
        public static Congratulations Instance { get; private set; }

        public GameObject panel;
        public TextMeshProUGUI towerName;
        public TextMeshProUGUI towerPrice;
        public Image towerPreview;
        public Image nextButton;

        private void Awake()
        {
            Instance = this;
        }

        public void Show(TowerData data)
        {
            towerName.text = data.title;
            towerPrice.text = data.price.ToString();
            towerPreview.sprite = data.renderShot;
            
            panel.SetActive(true);
        }

        public void OnHover()
        {
            nextButton.color = new Color(0.89960784f, 0.89960784f, 0.89960784f);
        }
        
        public void OnLeave()
        {
            nextButton.color = Color.white;
        }

        public void OnClick()
        {
            panel.SetActive(false);
            LevelManager.Instance.LoadNextLevel();
        }
    }
}