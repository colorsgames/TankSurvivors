using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : ItemSpawnManager
{
    public static GoldSpawner Instance;

    private void Start()
    {
        Instance = this;
    }
}
