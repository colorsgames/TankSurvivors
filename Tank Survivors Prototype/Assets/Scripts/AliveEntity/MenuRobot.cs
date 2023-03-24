using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRobot : NPC
{
    [SerializeField] float lerpSpeed;
    [SerializeField] float endValue;
    [SerializeField] Vector2 target;

    public override void Update()
    {
        if (Alive)
        {
            if (target.y >= 0.9f)
                endValue *= -1;
            if (target.y <= -0.9f)
                endValue *= -1;

            target.y = Mathf.Lerp(target.y, endValue, lerpSpeed * Time.deltaTime);

            Following(target + (Vector2)transform.position, 0);
            tower.SetTarget(transform.up + transform.position);
            weapons.StartShooting();
        }
        base.Update();
    }
}
