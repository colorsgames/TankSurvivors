using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : NPC
{
    [SerializeField] private float radiusFollowPoint;
    [SerializeField] private float deadTime;

    Player player;

    Vector2 followTarget;

    int placeId;

    bool following;

    public override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
        //player.IncreaseAlly();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), boxCollider);
        placeId = player.GetFreePlaceID();
        StartFollowing(placeId);
    }

    public override void Update()
    {
        if (Alive)
        {
            if (following)
            {
                tower.SetTarget(player.TowerTarget);

                Vector2 fPos = (Vector2)player.transform.position + followTarget;
                Following(fPos, radiusFollow);

                if ((Input.GetMouseButton(0) && !PlatformManager.instance.IsMobile) || player.TowerJS.Shoot)
                {
                    weapons.StartShooting();
                }
            }
        }
        base.Update();
    }

    void StartFollowing(int id)
    {
        following = true;
        switch (id)
        {
            case 0:
                followTarget = Vector2.up * radiusFollowPoint;
                break;
            case 1:
                followTarget = Vector2.right * radiusFollowPoint;
                break;
            case 2:
                followTarget = Vector2.left * radiusFollowPoint;
                break;
            case 3:
                followTarget = Vector2.down * radiusFollowPoint;
                break;
        }
    }
}
