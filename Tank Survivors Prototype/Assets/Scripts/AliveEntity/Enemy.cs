using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Enemy : NPC
{
    protected Transform target;

    [SerializeField] private GameObject[] items;

    [SerializeField] private int spawnItemsCoefficient = 15;

    [SerializeField] private float radiusAttack = 12;

    public override void Start()
    {
        target = FindObjectOfType<Player>().transform;
        base.Start();
    }

    private void Update()
    {
        if (!Alive) return;
        Following(target.position, radiusFollow);
        tower.SetTarget(target.position);

        float dist = (target.position - transform.position).magnitude;
        if (dist < radiusAttack)
            weapons.StartShooting();
    }

    public override void Dead()
    {
        GameManager.instance.UpKillings();
        SpawnItem();
        base.Dead();
    }

    void SpawnItem()
    {
        int randId = Random.Range(0, items.Length + spawnItemsCoefficient);
        if (randId < items.Length)
            Instantiate(items[randId], transform.position, Quaternion.identity);
    }
}
