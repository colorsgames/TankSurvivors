using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpaSpawnManager : ItemSpawnManager
{
    public static ExpaSpawnManager Instance;

    private void Start()
    {
        Instance = this;
    }
}
