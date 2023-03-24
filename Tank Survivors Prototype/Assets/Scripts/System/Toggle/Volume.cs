using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : Toggle
{
    public override void Start()
    {
        state = Progress.Instance.progressInfo.toggleState1;
        SoundManager.soundPause = !state;
        AudioListener.pause = !state;
        base.Start();
    }

    public override void Use()
    {
        base.Use();
        Progress.Instance.progressInfo.toggleState1 = state;
        SoundManager.soundPause = !state;
        AudioListener.pause = !state;
        Progress.Instance.Save();
    }
}
