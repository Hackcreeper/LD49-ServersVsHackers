using System;
using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public static Coins Instance { get; private set; }
    
    public int amount;
    public TextMeshProUGUI amountText;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        amountText.text = amount.ToString();
    }
}