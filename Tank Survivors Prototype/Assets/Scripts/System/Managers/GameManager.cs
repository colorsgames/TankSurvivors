using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static UnityEvent decreaseSpawnRate = new UnityEvent();

    public int Killings { get { return killings; } }
    public int Money { get { return moneyForThisGame; } }

    [SerializeField] private GameModeData gameModeData;

    [SerializeField] private GameObject resultsPanel;
    [SerializeField] private GameObject counters;
    [SerializeField] private GameObject mobileController;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject newLVLPanel;

    [SerializeField] private TMP_Text killingsTMP;
    [SerializeField] private TMP_Text moneyTMP;
    [SerializeField] private TMP_Text statsKillingsTMP;
    [SerializeField] private TMP_Text statsMoneyTMP;
    [SerializeField] private TMP_Text statsTotalMoneyTMP;
    [SerializeField] private TMP_Text statsMinutesTMP;
    [SerializeField] private TMP_Text statsSecondsTMP;

    [SerializeField] private int invokeKills;

    int killings;
    int moneyForThisGame;

    bool isPause;
    bool isNewLVL;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
        killingsTMP.text = "0";
        moneyTMP.text = GameCurrencyData.TotalMoney.ToString();
        resultsPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && !isNewLVL)
        {
            if (!isPause)
                Pause(true);
            else
                Pause(false);
        }
    }

    public void UpKillings()
    {
        killings++;
        killingsTMP.text = killings.ToString();
    }

    public void UpMoney(int value)
    {
        moneyForThisGame += value;
        moneyTMP.text = (moneyForThisGame + GameCurrencyData.TotalMoney).ToString();
    }

    public void OpenResults()
    {
        resultsPanel.SetActive(true);

        pause.SetActive(false);
        UpdateStats();

        UIAimController.instance.AimDeactivate(true);
        counters.SetActive(false);
        Time.timeScale = 0;
    }

    public void OpenNewLVLPanel(bool value)
    {
        newLVLPanel.SetActive(value);
        isNewLVL = value;
        UIAimController.instance.AimDeactivate(value);
    }

    public void ExitInMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause(bool value)
    {
        if (PlatformManager.Instance.IsMobile)
        {
            mobileController.SetActive(value);
        }
        pause.SetActive(value);
        isPause = value;
        UIAimController.instance.AimDeactivate(value);
        StopTime(value);
    }

    public void StopTime(bool value)
    {
        if (value)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void UpdateStats()
    {
        statsKillingsTMP.text = killings.ToString();
        statsMoneyTMP.text = moneyForThisGame.ToString();
        statsMinutesTMP.text = TimeCounter.Instance.Minutes;
        statsSecondsTMP.text = TimeCounter.Instance.Seconds;
        GameCurrencyData.IncreaseTotalMoney(moneyForThisGame);
        statsTotalMoneyTMP.text = GameCurrencyData.TotalMoney.ToString();
        GameCurrencyData.SaveMoney();
    }
}
