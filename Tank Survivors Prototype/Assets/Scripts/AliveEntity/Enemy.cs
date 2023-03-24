using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Enemy : NPC
{
    protected Transform target;

    [SerializeField] private int spawnItemsCoefficient = 3;
    [SerializeField] protected float respawnDelay = 30;

    protected float currentTime;

    public override void Start()
    {
        if (!target)
        {
            target = FindObjectOfType<Player>().transform;
        }
        currentTime = respawnDelay;
        base.Start();
    }

    public override void Destroy()
    {
        GameManager.Instance.UpKillings();
        SpawnItem();
        base.Destroy();
    }

    void SpawnItem()
    {
        int randId = Random.Range(1, spawnItemsCoefficient);
        if (randId == 1)
            GoldSpawner.Instance.GetItem(transform.position);
        if (randId == 2)
            ExpaSpawnManager.Instance.GetItem(transform.position);
    }
}
