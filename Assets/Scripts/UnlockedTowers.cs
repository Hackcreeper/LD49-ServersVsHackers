using System.Collections.Generic;
using Towers;
using UnityEngine;

public class UnlockedTowers : MonoBehaviour
{
    public static UnlockedTowers Instance { get; private set; }
    
    public TowerData[] defaultTowers;
    public int unlockedLevel = 1;

    private List<TowerData> _towers = new List<TowerData>();

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        _towers.AddRange(defaultTowers);
        
        DontDestroyOnLoad(gameObject);
    }

    public TowerData[] GetAllTowers() => _towers.ToArray();

    public void AddTower(TowerData tower)
    {
        _towers.Add(tower);
    }

    public void Unlock(int lvl)
    {
        if (lvl > unlockedLevel)
        {
            unlockedLevel = lvl;
        }
    }
}