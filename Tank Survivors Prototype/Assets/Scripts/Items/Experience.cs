using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : Items
{
    [SerializeField] private float expa;

    public override void Use()
    {
        ExperienceManager.Instance.AddExp(expa);
        base.Use();
    }
}
