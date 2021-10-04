using TMPro;
using UnityEngine;

namespace UI
{
    public class Banner : MonoBehaviour
    {
        public static Banner Instance { get; private set; }

        public GameObject introBanner;
        public GameObject winBanner;
        public TextMeshProUGUI levelNameText;

        private void Awake()
        {
            Instance = this;
        }

        public void ShowIntroBanner()
        {
            levelNameText.text = $"Level {LevelManager.Instance.GetCurrentLevel()}";
            introBanner.SetActive(true);
        }
        
        public void HideIntroBanner()
        {
            introBanner.SetActive(false);
        }
    }
}