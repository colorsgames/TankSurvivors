using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Items : MonoBehaviour
{
    private IObjectPool<Items> pool;

    [SerializeField] private float destroyTime = 30;
    [SerializeField] private float speed = 5;
    [SerializeField] private float sizeSpeed = 5;

    [SerializeField] private AudioClip sound;

    protected Player player;

    ItemSpawnManager spawnManager;

    Vector3 maxSize;

    float currentDestroyTime;

    bool fly;
    bool spawn;

    public virtual void Start()
    {
        maxSize = transform.localScale;
        transform.localScale = Vector3.zero;
        currentDestroyTime = destroyTime;
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (fly)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, (speed + player.GetSpeed()) * Time.deltaTime);
            Vector3 offset = player.transform.position - transform.position;
            if (offset.magnitude <= 0.5)
                Use();
        }
        else
        {
            currentDestroyTime -= Time.deltaTime;
            if(currentDestroyTime <= 0)
            {
                currentDestroyTime = destroyTime;
                Dead();
            }
        }
        if (spawn)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, maxSize, sizeSpeed * Time.deltaTime);
        }
    }

    public virtual void Use()
    {
        SoundManager.Instance.PlayAudioClip(sound);
        Dead();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            fly = true;
        }
    }

    public void SetPool(IObjectPool<Items> _pool) => pool = _pool;

    public void SetSpawnManager(ItemSpawnManager sm)
    {
        spawnManager = sm;
    }

    public void HideObject(bool value)
    {
        gameObject.SetActive(!value);
        if (value)
        {
            transform.localScale = Vector3.zero;
            fly = false;
        }
        spawn = !value;
    }

    void Dead()
    {
        spawnManager.DecreaseCurrentCount();
        pool.Release(this);
    }
}
