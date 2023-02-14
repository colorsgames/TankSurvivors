using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[Serializable]
public class TextInfo
{
    public TMP_Text tmp;

    public string ruText, engText;

    public void SetText(bool value)
    {
        tmp.text = value ? engText : ruText;
    }
}
