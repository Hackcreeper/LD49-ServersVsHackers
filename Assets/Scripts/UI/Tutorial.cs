using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace UI
{
    public class Tutorial : MonoBehaviour
    {
        public GameObject card1;
        public GameObject card2;

        private bool _towerPlaced;

        private void Start()
        {
            card1.SetActive(true);
            card2.SetActive(false);
        }

        public void TowerSelected()
        {
            if (_towerPlaced)
            {
                return;
            }

            _towerPlaced = true;
            card1.SetActive(false);
            card2.SetActive(true);
        }

        public void DisableTutorial()
        {
            card1.SetActive(false);
            card2.SetActive(false);
        }
    }
}