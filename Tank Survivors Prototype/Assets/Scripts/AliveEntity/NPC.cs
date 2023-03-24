using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class NPC : AliveEntity
{
    private IObjectPool<NPC> pool;

    [SerializeField] protected float acceleration = 2;
    [SerializeField] protected float radiusFollow = 5;
    [SerializeField] private float destroyTime = 0.5f;

    [SerializeField] private TrailRenderer[] trails;

    protected BoxCollider2D boxCollider;

    NPCSpawnManager spawnManager;

    float currentDestroyTime;

    public override void Awake()
    {
        currentDestroyTime = destroyTime;
        boxCollider = GetComponent<BoxCollider2D>();
        base.Awake();
    }

    public virtual void Update()
    {
        if (!Alive)
        {
            currentDestroyTime -= Time.deltaTime;
            if (currentDestroyTime <= 0)
            {
                currentDestroyTime = destroyTime;
                //tower.gameObject.SetActive(false);
                PoolRelease();
            }
        }

    }

    public override void Destroy()
    {
        //spawnManager.DecreaseCurrentCount();
        boxCollider.enabled = false;
        base.Destroy();
    }

    public void SetSpawnManager(NPCSpawnManager sm)
    {
        spawnManager = sm;
    }

    public bool Following(Vector3 target, float radius)
    {
        Vector2 dir = target - transform.position;
        Vector2 newDir = new Vector2(dir.y, dir.x);
        if (dir.magnitude > radius)
        {
            movementInput = Vector2.Lerp(movementInput, newDir, acceleration * Time.deltaTime);
            return true;
        }
        else
        {
            movementInput = Vector2.Lerp(movementInput, Vector2.zero, acceleration * Time.deltaTime);
            return false;
        }
    }

    public void SetPool(IObjectPool<NPC> _pool) => pool = _pool;

    public virtual void HideObject(bool value)
    {
        gameObject.SetActive(!value);
        if (tower)
            tower.gameObject.SetActive(!value);
        boxCollider.enabled = !value;
        Alive = !value;
        Rb.isKinematic = value;
        if (!value && tower)
            tower.transform.position = transform.position;
        ResetHealth();
    }

    protected void PoolRelease()
    {
        foreach (var item in trails)
        {
            item.Clear();
        }
        spawnManager.DecreaseCurrentCount();
        pool.Release(this);
    }
}
