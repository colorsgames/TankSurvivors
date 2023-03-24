using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : Enemy
{
    Vector2 startPlayerPos;

    bool isFollow;

    public override void Update()
    {
        if (Alive)
        {
            isFollow = Following(startPlayerPos, radiusFollow);
            if (!isFollow)
            {
                Following(target.position, radiusFollow);
            }
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                Dead();
                currentTime = respawnDelay;
            }
        }
        base.Update();
    }

    public override void HideObject(bool value)
    {
        if (!target)
        {
            target = FindObjectOfType<Player>().transform;
        }
        if (!value)
            startPlayerPos = target.position;
        base.HideObject(value);
    }
}
