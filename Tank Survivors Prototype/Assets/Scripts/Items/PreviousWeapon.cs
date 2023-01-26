using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousWeapon : Items
{
    public override void Use()
    {
        player.PreviousWeapon();
        base.Use();
    }
}
