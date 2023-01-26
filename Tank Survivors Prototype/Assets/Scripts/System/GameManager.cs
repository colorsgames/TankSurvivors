using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int Killings { get { return killings; } }
    public int Money { get { return moneyForThisGame; } }

    [SerializeField] private GameModeData gameModeData;

    [SerializeField] private GameObject resultsPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    [SerializeField] private TMP_Text killingsTMP;
    [SerializeField] private TMP_Text moneyTMP;
    [SerializeField] private TMP_Text statsKillingsTMP;
    [SerializeField] private TMP_Text statsMoneyTMP;
    [SerializeField] private TMP_Text statsTotalMoneyTMP;
    [SerializeField] private TMP_Text statsProfitRatioTMP;
    [SerializeField] private TMP_Text statsProfitRatioAndMoneyForThisGameTMP;

    int killings;
    int moneyForThisGame;
    int profitRatio;

    private void Awake()
    {
        instance = this;
        killingsTMP.text = "0";
        moneyTMP.text = "0";
        profitRatio = gameModeData.profitRatio;
        resultsPanel.SetActive(false);
    }

    public void UpKillings()
    {
        killings++;
        killingsTMP.text = killings.ToString();
    }

    public void UpMoney()
    {
        moneyForThisGame++;
        moneyTMP.text = moneyForThisGame.ToString();
    }

    public void OpenResults(bool value)
    {
        resultsPanel.SetActive(true);

        if (value)
            winPanel.SetActive(true);
        else
        {
            losePanel.SetActive(true); 
            profitRatio = 1;
        }

        UpdateStats();

        UIAimController.instance.DestroyAim();
        Time.timeScale = 0;
    }

    public void ExitInMenu()
    {
        SceneManager.LoadScene(0);
    }

    void UpdateStats()
    {
        statsKillingsTMP.text = killings.ToString();
        statsMoneyTMP.text = moneyForThisGame.ToString();
        statsProfitRatioTMP.text = "x" + profitRatio.ToString();
        int profitMoney = moneyForThisGame * profitRatio;
        statsProfitRatioAndMoneyForThisGameTMP.text = profitMoney.ToString();
        GameCurrencyData.IncreaseTotalMoney(profitMoney);
        statsTotalMoneyTMP.text = GameCurrencyData.TotalMoney.ToString();
    }
}
