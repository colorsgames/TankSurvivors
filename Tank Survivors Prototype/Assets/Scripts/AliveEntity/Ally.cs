using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : NPC
{
    [SerializeField] private float radiusFollowPoint;
    [SerializeField] private float deadTime;
    [SerializeField] private float attackDist;

    List<Enemy> visEnemy = new List<Enemy>();

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
                if (visEnemy.Count > 0)
                {
                    for (int i = 0; i < visEnemy.Count; i++)
                    {
                        if (!visEnemy[i].Alive)
                        {
                            visEnemy.Remove(visEnemy[i]);
                        }
                    }
                }

                if (visEnemy.Count < 1)
                {
                    tower.SetTarget(transform.up + transform.position);
                }
                else
                {
                    int attackId = -1;
                    for (int i = 0; i < visEnemy.Count; i++)
                    {
                        Vector2 offset = visEnemy[i].transform.position - transform.position;
                        float dist = offset.magnitude;
                        if (dist <= attackDist)
                        {
                            attackId = i;
                        }
                    }
                    Vector2 target = visEnemy[attackId].transform.position;
                    tower.SetTarget(target);

                    weapons.StartShooting();
                }

                Vector2 fPos = (Vector2)player.transform.position + followTarget;
                Following(fPos, radiusFollow);

                /*if ((Input.GetMouseButton(0) && !PlatformManager.instance.IsMobile) || player.TowerJS.Shoot)
                {
                    weapons.StartShooting();
                }*/
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
            visEnemy.Add(collision.GetComponent<Enemy>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
            visEnemy.Remove(collision.GetComponent<Enemy>());
    }
}
