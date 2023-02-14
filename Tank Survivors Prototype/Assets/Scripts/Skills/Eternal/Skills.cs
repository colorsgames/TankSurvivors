using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace EternalSkills
{
    public abstract class Skills : MonoBehaviour
    {
        public string Descritpion { get { return LanguageManager.isEng ? engDescription : ruDescription; } }

        public int Price { get { return currentPrice; } }

        public bool StepsExhausted { get; private set; }
        public bool InsufficientFunds { get; private set; }

        [SerializeField] private SkillLevelData skillLevelData;
        [SerializeField] private TMP_Text lvlTMP;
        [SerializeField] private string ruLvl = "Óð.: ", engLvl = "Lvl: ";
        [SerializeField] private string ruDescription, engDescription;

        [SerializeField] private float endValue;
        [SerializeField] private int startPrice = 10;
        [SerializeField] private float priceIncreaseCoefficient = 1.5f;

        [SerializeField] private bool isDecreaseValue;

        [SerializeField] protected float percent;
        [SerializeField] protected int level;

        protected float currentValue;

        int currentPrice;

        private void Awake()
        {
            UpgradeManager.onButtonDown.AddListener(FoundsCheck);
            SkillsManager.StartGame.AddListener(SetSkillData);
            LanguageManager.onChangeLang.AddListener(UpdateLevel);
        }

        public virtual void Start()
        {
            level = PlayerPrefs.GetInt("step" + gameObject.name);
            UpdateLevel();
            currentPrice = PlayerPrefs.GetInt(gameObject.name);

            if (currentPrice == 0)
            {
                currentPrice = startPrice;
            }

            StepCheck();
            FoundsCheck();
        }

        public virtual void Use()
        {
            level++;
            UpdateLevel();
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
            PlayerPrefs.SetInt(gameObject.name, currentPrice);
        }

        public void ShowIntfo()
        {
            UpgradeManager.Instance.SelectUpgrade(this);
        }

        void UpdateLevel()
        {
            lvlTMP.text = LanguageManager.isEng ? engLvl : ruLvl + level;
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
            StepsExhausted = true;
        }

        void FoundsCheck()
        {
            if (GameCurrencyData.TotalMoney < currentPrice)
            {
                InsufficientFunds = true;
            }
            else
            {
                InsufficientFunds = false;
            }
        }
    }
}