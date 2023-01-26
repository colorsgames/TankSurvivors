using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : AliveEntity
{
    [SerializeField] protected float acceleration = 2;
    [SerializeField] protected float radiusFollow = 5;
    [SerializeField] private float destroyTime = 1;

    protected BoxCollider2D boxCollider;

    NPCSpawnManager spawnManager;

    public override void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        base.Start();
    }

    public override void Dead()
    {
        boxCollider.enabled = false;
        spawnManager.DecreaseCurrentCount();
        Destroy(gameObject, destroyTime);
        Destroy(tower.gameObject, destroyTime);
        base.Dead();
    }

    public void SetSpawnManager(NPCSpawnManager sm)
    {
        spawnManager = sm;
    }

    public void Following(Vector3 target, float radius)
    {
        Vector2 dir = target - transform.position;
        Vector2 newDir = new Vector2(dir.y, dir.x);
        if (dir.magnitude > radius)
        {
            movementInput = Vector2.Lerp(movementInput, newDir, acceleration * Time.deltaTime);
        }
        else
        {
            movementInput = Vector2.Lerp(movementInput, Vector2.zero, acceleration * Time.deltaTime);
        }
    }
}
