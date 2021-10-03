using UnityEngine;

namespace Enemies
{
    [System.Serializable]
    public struct PlannedEnemy
    {
        public int time;
        public GameObject enemyPrefab;
        
        public int amount;
    }
}