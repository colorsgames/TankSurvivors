using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Items
{
    [SerializeField] private int maxMoney;

    public override void Use()
    {
        GameManager.Instance.UpMoney(maxMoney);
        base.Use();
    }
}
