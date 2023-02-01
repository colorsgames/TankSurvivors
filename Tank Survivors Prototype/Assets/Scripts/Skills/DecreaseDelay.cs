using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseDelay : Skills
{
    [SerializeField] private float delay;

    public override void Use()
    {
        SkillsManager.instance.DecreaseDelay(delay);
        base.Use();
    }
}
