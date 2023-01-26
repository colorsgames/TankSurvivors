using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class UpgradeManager : MonoBehaviour, IPointerDownHandler
{
    public static UpgradeManager instance;

    public static UnityEvent onButtonDown = new UnityEvent();

    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text money;
    [SerializeField] private GameObject unselected;
    [SerializeField] private GameObject maxStep;
    [SerializeField] private GameObject needMoney;

    [SerializeField] private Button button;

    Skills selectedSkills;

    private void Awake()
    {
        instance = this;
        //button = GetComponentInChildren<Button>();
    }

    public void SelectUpgrade(Skills skill)
    {
        selectedSkills = skill;
        descriptionText.text = selectedSkills.Descritpion;

        UpdateSelectedSkillState();
    }

    public void Upgrade()
    {
        selectedSkills.Use();
        onButtonDown.Invoke();
        UpdateSelectedSkillState();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Clear();
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
            money.text = selectedSkills.Price.ToString();
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
        descriptionText.text = string.Empty;
    }
}
