using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCount : Skills
{
    public override void Use()
    {
        SkillsManager.instance.IncreaseAllyCount();
        base.Use();
    }
}
