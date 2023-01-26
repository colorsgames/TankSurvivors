using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseWeaponDelay : Items
{
    [SerializeField] private float time;

    public override void Use()
    {
        player.DecreaseShotTime(time);
        base.Use();
    }
}
