using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooters : Enemy
{
    [SerializeField] private float radiusAttack = 6;
    [SerializeField] private float radiusRespawn = 80;

    public override void Update()
    {
        if (Alive)
        {
            Following(target.position, radiusFollow);
            if (tower)
                tower.SetTarget(target.position);

            float dist = (target.position - transform.position).magnitude;
            if (dist < radiusAttack && weapons)
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
}
