using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public RectTransform wrapper;
        public float smooth = 5f;

        private Vector2 _target;

        private void Start()
        {
            _target = wrapper.anchoredPosition;
        }

        public void StartGame()
        {
            LevelManager.Instance.StartGameInLevel(
                UnlockedTowers.Instance.unlockedLevel                
            );
        }

        public void ToMainMenu()
        {
            _target = new Vector2(0, 0);
        }

        public void ToLevelSelection()
        {
            _target = new Vector2(-1550, 0);
        }

        private void Update()
        {
            wrapper.anchoredPosition = Vector2.Lerp(
                wrapper.anchoredPosition,
                _target,
                smooth * Time.deltaTime
            );
        }
    }
}