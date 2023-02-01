using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawnRate : Skills
{
    [SerializeField] private int time;

    public override void Use()
    {
        SkillsManager.instance.DecreaseAllySpawnRate(time);
        base.Use();
    }
}
