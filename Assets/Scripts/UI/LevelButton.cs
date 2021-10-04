using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image), typeof(Button))]
    public class LevelButton : MonoBehaviour
    {
        public int id;
        public Sprite unlocked;
        public Sprite locked;

        private void Start()
        {
            GetComponent<Image>().sprite = IsUnlocked() ? unlocked : locked;
            GetComponent<Button>().interactable = IsUnlocked();
        }

        private bool IsUnlocked() => UnlockedTowers.Instance.unlockedLevel >= id;
        
        public void GoInto()
        {
            LevelManager.Instance.StartGameInLevel(id);
        }
    }
}