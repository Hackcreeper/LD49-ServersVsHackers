using System.Collections.Generic;
using Towers;
using UnityEngine;

public class UnlockedTowers : MonoBehaviour
{
    public static UnlockedTowers Instance { get; private set; }
    
    public TowerData[] defaultTowers;

    private List<TowerData> _towers = new List<TowerData>();

    private void Awake()
    {
        Instance = this;
        _towers.AddRange(defaultTowers);
    }

    public TowerData[] GetAllTowers() => _towers.ToArray();
}