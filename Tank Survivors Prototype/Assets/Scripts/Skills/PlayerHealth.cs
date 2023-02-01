using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Skills
{
    [SerializeField] private int health;

    public override void Use()
    {
        SkillsManager.instance.IncreaseHealth(health);
        base.Use();
    }
}
