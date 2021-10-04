using TMPro;
using UnityEngine;

namespace UI
{
    public class Banner : MonoBehaviour
    {
        public static Banner Instance { get; private set; }

        public TextMeshProUGUI levelNameText;

        private void Awake()
        {
            Instance = this;
        }

        public void UpdateLevelName()
        {
            levelNameText.text = $"Level {LevelManager.Instance.GetCurrentLevel()}";
        }
    }
}