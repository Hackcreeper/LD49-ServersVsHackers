using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameOver : MonoBehaviour
    {
        public static GameOver Instance { get; private set; }

        public GameObject panel;
        public Image buttonRetry;
        public Image buttonMenu;
        public AudioClip gameOverClip;
        
        private void Awake()
        {
            Instance = this;
        }

        public void Show()
        {
            panel.SetActive(true);
            Audio.Instance.Play(gameOverClip);
        }
        
        public void Retry()
        {
            panel.SetActive(false);
            LevelManager.Instance.RestartLevel();
        }

        public void OnHoverRetry()
        {
            buttonRetry.color = new Color(0.89960784f, 0.89960784f, 0.89960784f);
        }
        
        public void OnLeaveRetry()
        {
            buttonRetry.color = Color.white;
        }

        public void Menu()
        {
            panel.SetActive(false);
            SceneManager.LoadScene("MainMenu");
        }
        
        public void OnHoverMenu()
        {
            buttonMenu.color = new Color(0.89960784f, 0.89960784f, 0.89960784f);
        }

        public void OnLeaveMenu()
        {
            buttonMenu.color = Color.white;
        }
    }
}