using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LanguageManager : MonoBehaviour
{
    public static UnityEvent onChangeLang = new UnityEvent();

    public static bool isEng = true;

    [SerializeField] TextInfo[] text;

    private void Awake()
    {
        onChangeLang.AddListener(UpdateText);
    }

    void Start()
    {
        UpdateText();
    }

    void UpdateText()
    {
        foreach (var item in text)
        {
            item.SetText(isEng);
        }
    }
}

