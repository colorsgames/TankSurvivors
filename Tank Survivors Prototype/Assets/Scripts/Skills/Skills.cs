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

    [SerializeField] ColorData colors;

    [SerializeField] private string description;

    [SerializeField] private int maxSteps;
    [SerializeField] private int startPrice;
    [SerializeField] private float increaseCoefficient;

    Color red;
    Color green;

    protected int step;
    int currentPrice;

    private void Awake()
    {
        UpgradeManager.onButtonDown.AddListener(FoundsCheck);

        red = colors.red;
        green = colors.green;
    }

    private void Start()
    {
        step = PlayerPrefs.GetInt("step" + gameObject.name);
        currentPrice = PlayerPrefs.GetInt(gameObject.name);

        if (currentPrice == 0)
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
        PlayerPrefs.SetInt("step" + gameObject.name, step);
        GameCurrencyData.DecreaseTotalMoney(currentPrice);
        GameCurrencyData.SaveMoney();
        NextPrice();
        StepCheck();
    }

    public void NextPrice()
    {
        currentPrice += (int)(currentPrice / increaseCoefficient);
        UpdatePrice();
        PlayerPrefs.SetInt(gameObject.name, currentPrice);
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
        if (GameCurrencyData.TotalMoney < currentPrice)
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
