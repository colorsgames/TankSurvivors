using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyHealth : Skills
{
    [SerializeField] private int health;

    public override void Use()
    {
        SkillsManager.instance.IncreaseAllyHealth(health);
        base.Use();
    }
}
