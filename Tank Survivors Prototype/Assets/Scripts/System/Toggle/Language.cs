using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : Toggle
{
    public override void Awake()
    {
        base.Awake();
        LanguageManager.isEng = state;
    }

    public override void Use()
    {
        base.Use();
        LanguageManager.isEng = state;
        LanguageManager.onChangeLang.Invoke();
    }
}
