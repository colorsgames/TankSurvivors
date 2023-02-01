using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDamage : Skills
{
    [SerializeField] private int damage;

    public override void Use()
    {
        SkillsManager.instance.IncreaseDamage(damage);
        base.Use();
    }
}
