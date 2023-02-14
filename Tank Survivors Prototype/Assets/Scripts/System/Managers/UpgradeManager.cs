using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using EternalSkills;

public class UpgradeManager : MonoBehaviour, IPointerDownHandler
{
    public static UpgradeManager Instance;

    public static UnityEvent onButtonDown = new UnityEvent();

    [SerializeField] private TMP_Text descriptionTMP;
    [SerializeField] private TMP_Text buttonTextTMP;
    [SerializeField] private TMP_Text totalMoneyTMP;

    [SerializeField] private Color green, red;

    [SerializeField] private Button button;

    Skills selectedSkills;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        Instance = this;
        onButtonDown.AddListener(UpdateMoneyText);
        //button = GetComponentInChildren<Button>();
    }

    public void SelectUpgrade(Skills skill)
    {
        selectedSkills = skill;
        descriptionTMP.text = selectedSkills.Descritpion;
        UpdateSelectedSkillState();
    }

    public void Upgrade()
    {
        selectedSkills.Use();
        UpdateMoneyText();
        onButtonDown.Invoke();
        UpdateSelectedSkillState();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Clear();
    }

    void UpdateMoneyText()
    {
        GameCurrencyData.LoadMoney();
        totalMoneyTMP.text = GameCurrencyData.TotalMoney.ToString();
    }

    void UpdateSelectedSkillState()
    {
        if (selectedSkills.StepsExhausted)
        {
            button.enabled = false;
            buttonTextTMP.text = "FULL";
            buttonTextTMP.color = Color.white;
        }
        else if (selectedSkills.InsufficientFunds)
        {
            button.enabled = false;
            buttonTextTMP.text = selectedSkills.Price.ToString();
            buttonTextTMP.color = red;
        }
        else
        {
            button.enabled = true;
            buttonTextTMP.text = selectedSkills.Price.ToString();
            buttonTextTMP.color = green;
        }
    }

    void Clear()
    {
        button.enabled = false;
        buttonTextTMP.text = String.Empty;
        descriptionTMP.text = string.Empty;
    }
}
