using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace EternalSkills
{
    public abstract class Skills : MonoBehaviour
    {
        public string Descritpion { get { return description; } }

        public int Price { get { return currentPrice; } }

        public bool StepsExhausted { get; private set; }
        public bool InsufficientFunds { get; private set; }

        [SerializeField] private SkillLevelData skillLevelData;
        [SerializeField] private GameObject blockLimit;
        [SerializeField] private GameObject price;

        [SerializeField] private TMP_Text priceTMP;

        [SerializeField] ColorData colors;

        [SerializeField] private string description;

        [SerializeField] private float endValue;
        [SerializeField] private int startPrice;
        [SerializeField] private float priceIncreaseCoefficient;

        [SerializeField] private bool isDecreaseValue;

        [SerializeField] protected float percent;
        [SerializeField] protected int level;

        Color red;
        Color green;

        protected float currentValue;

        int currentPrice;

        private void Awake()
        {
            UpgradeManager.onButtonDown.AddListener(FoundsCheck);
            SkillsManager.StartGame.AddListener(SetSkillData);
            red = colors.red;
            green = colors.green;
        }

        public virtual void Start()
        {
            level = PlayerPrefs.GetInt("step" + gameObject.name);
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
            level++;
            PlayerPrefs.SetInt("step" + gameObject.name, level);
            GameCurrencyData.DecreaseTotalMoney(currentPrice);
            GameCurrencyData.SaveMoney();
            NextPrice();
            StepCheck();
        }

        public void SetSkillData()
        {
            if (skillLevelData)
            {
                skillLevelData.level = level;
                skillLevelData.isMax = false;
            }
        }

        public void NextPrice()
        {
            currentPrice += (int)(currentPrice / priceIncreaseCoefficient);
            UpdatePrice();
            PlayerPrefs.SetInt(gameObject.name, currentPrice);
        }

        public void ShowIntfo()
        {
            UpgradeManager.Instance.SelectUpgrade(this);
        }

        void StepCheck()
        {
            if (isDecreaseValue)
            {
                if(currentValue <= endValue)
                {
                    Block();
                }
            }
            else
            {
                if(currentValue>= endValue)
                {
                    Block();
                }
            }
        }

        void Block()
        {
            blockLimit.SetActive(true);
            StepsExhausted = true;
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
}