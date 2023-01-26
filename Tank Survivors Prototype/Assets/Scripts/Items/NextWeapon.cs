using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWeapon : Items
{
    public override void Use()
    {
        player.NextWeapon();
        base.Use();
    }
}
