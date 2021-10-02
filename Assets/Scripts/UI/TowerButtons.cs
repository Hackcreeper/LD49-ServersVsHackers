using System;
using UnityEngine;

namespace UI
{
    public class TowerButtons : MonoBehaviour
    {
        public GameObject towerButtonPrefab;

        private void Start()
        {
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