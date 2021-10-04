using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class Pause : MonoBehaviour
    {
        public static Pause Instance { get; private set; }

        public GameObject menu;

        private bool paused;

        private void Awake()
        {
            Instance = this;
        }

        public void PauseGame()
        {
            paused = true;
            Time.timeScale = 0;

            menu.SetActive(true);
        }

        public void UnpauseGame()
        {
            paused = false;
            Time.timeScale = 1f;

            menu.SetActive(false);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape) &&
                !Input.GetKeyDown(KeyCode.Backspace) &&
                !Input.GetKeyDown(KeyCode.P)
            )
            {
                return;
            }

            if (paused)
            {
                UnpauseGame();
                return;
            }
            
            PauseGame();
        }

        public void BackToMainMenu()
        {
            UnpauseGame();
            
            SceneManager.LoadScene("MainMenu");
        }

        public void RestartLevel()
        {
            UnpauseGame();
            LevelManager.Instance.RestartLevel();
        }
    }
}