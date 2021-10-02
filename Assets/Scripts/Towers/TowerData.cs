using UnityEngine;

namespace Towers
{
    [CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/Tower", order = 1)]
    public class TowerData : ScriptableObject
    {
        public GameObject prefab;
        public string title;
        public int price;
    }
}