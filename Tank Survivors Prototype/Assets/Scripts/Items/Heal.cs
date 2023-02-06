using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Items
{
    [SerializeField] private float healValue;

    public override void Use()
    {
        player.Healing(healValue);
        base.Use();
    }
}
