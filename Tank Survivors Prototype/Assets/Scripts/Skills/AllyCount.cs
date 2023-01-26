using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCount : Skills
{
    [SerializeField] private SpawnAliveEntityData allySpawnData;

    public override void Use()
    {
        allySpawnData.maxCount++;
        base.Use();
    }
}
