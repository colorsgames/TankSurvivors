using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Enemy : NPC
{
    protected Transform target;

    [SerializeField] private int spawnItemsCoefficient = 15;

    [SerializeField] private float radiusAttack = 12;
    [SerializeField] private float radiusRespawn = 100;
    [SerializeField] private float respawnDelay = 30;

    float currentTime;

    public override void Start()
    {
        target = FindObjectOfType<Player>().transform;
        currentTime = respawnDelay;
        base.Start();
    }

    public override void Update()
    {
        if (Alive)
        {
            Following(target.position, radiusFollow);
            tower.SetTarget(target.position);

            float dist = (target.position - transform.position).magnitude;
            if (dist < radiusAttack)
                weapons.StartShooting();
            if (dist > radiusRespawn)
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 0)
                    PoolRelease();
            }
            else
                currentTime = respawnDelay;
        }
        base.Update();
    }

    public override void Dead()
    {
        GameManager.Instance.UpKillings();
        SpawnItem();
        base.Dead();
    }

    void SpawnItem()
    {
        int randId = Random.Range(0, spawnItemsCoefficient);
        if (randId == 1)
            GoldSpawner.Instance.GetItem(transform.position);
        if(randId == 2)
            ExpaSpawnManager.Instance.GetItem(transform.position);
    }
}
