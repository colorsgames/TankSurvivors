using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpSpeed : Skills
{
    [SerializeField] private float speed;

    public override void Use()
    {
        SkillsManager.instance.UpSpeed(speed);
        base.Use();
    }
}
