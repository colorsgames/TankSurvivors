using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyTMP;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        GameCurrencyData.LoadMoney();
        UpdateMoneyText();
        UpgradeManager.onButtonDown.AddListener(UpdateMoneyText);
    }

    void UpdateMoneyText()
    {
        moneyTMP.text = GameCurrencyData.TotalMoney.ToString();
    }
}
