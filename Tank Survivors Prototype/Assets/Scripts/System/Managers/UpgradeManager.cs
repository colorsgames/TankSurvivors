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
    [SerializeField] private TMP_Text priceBarTMP;
    [SerializeField] private TMP_Text totalMoneyTMP;
    [SerializeField] private GameObject unselected;
    [SerializeField] private GameObject maxStep;
    [SerializeField] private GameObject needMoney;

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
            maxStep.SetActive(true);
            unselected.SetActive(false);
            needMoney.SetActive(false);
        }
        else if (selectedSkills.InsufficientFunds)
        {
            button.enabled = false;
            maxStep.SetActive(false);
            unselected.SetActive(false);
            needMoney.SetActive(true);
            priceBarTMP.text = selectedSkills.Price.ToString();
        }
        else
        {
            button.enabled = true;
            maxStep.SetActive(false);
            unselected.SetActive(false);
            needMoney.SetActive(false);
        }
    }

    void Clear()
    {
        button.enabled = false;
        maxStep.SetActive(false);
        unselected.SetActive(true);
        needMoney.SetActive(false);
        descriptionTMP.text = string.Empty;
    }
}
