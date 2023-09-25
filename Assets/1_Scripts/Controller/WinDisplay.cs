using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinDisplay : MonoBehaviour
{
    public Text txtGoldBonus;
    [SerializeField] LuckyBonus luckyBonus;
    private void Start()
    {
        luckyBonus.OnChoiseSuccess += value =>
        {
            txtGoldBonus.text = value.ToString();
        };
    }
}
