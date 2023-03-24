using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : Toggle
{
    public override void Start()
    {
        state = Progress.Instance.progressInfo.toggleState0;
        base.Start();
        LanguageManager.isEng = state;
        LanguageManager.onChangeLang.Invoke();
    }

    public override void Use()
    {
        base.Use();
        Progress.Instance.progressInfo.toggleState0 = state;
        LanguageManager.isEng = state;
        LanguageManager.onChangeLang.Invoke();
        Progress.Instance.Save();
    }
}
