using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Skills : MonoBehaviour
{
    public string Descritpion { get { return description; } }

    public int Price { get { return currentPrice; } }

    public bool StepsExhausted { get; private set; }
    public bool InsufficientFunds { get; private set; }

    [SerializeField] private GameObject blockLimit;
    [SerializeField] private GameObject price;

    [SerializeField] private TMP_Text priceTMP;
    [SerializeField] Color red;
    [SerializeField] Color green;

    [SerializeField] private string description;

    [SerializeField] private int maxSteps;
    [SerializeField] private int startPrice;
    [SerializeField] private float increaseCoefficient;

    int step;
    int currentPrice;

    private void Awake()
    {
        UpgradeManager.onButtonDown.AddListener(FoundsCheck);
    }

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        step = PlayerPrefs.GetInt("step");
        currentPrice = PlayerPrefs.GetInt("price");

        if(currentPrice == 0)
        {
            currentPrice = startPrice;
        }

        StepCheck();
        UpdatePrice();
        FoundsCheck();
    }

    public virtual void Use()
    {
        step++;
        PlayerPrefs.SetInt("step", step);
        GameCurrencyData.DecreaseTotalMoney(currentPrice);
        NextPrice();
        StepCheck();
    }

    public void NextPrice()
    {
        currentPrice += (int)(currentPrice / increaseCoefficient);
        UpdatePrice();
        PlayerPrefs.SetInt("price", currentPrice);
    }

    public void ShowIntfo()
    {
        UpgradeManager.instance.SelectUpgrade(this);
    }

    void StepCheck()
    {
        if (step >= maxSteps)
        {
            blockLimit.SetActive(true);
            StepsExhausted = true;
        }
    }

    void UpdatePrice()
    {
        priceTMP.text = currentPrice.ToString();
    }

    void FoundsCheck()
    {
        if(GameCurrencyData.TotalMoney < currentPrice)
        {
            priceTMP.color = red;
            InsufficientFunds = true;
        }
        else
        {
            priceTMP.color = green;
            InsufficientFunds = false;
        }
    }
}
