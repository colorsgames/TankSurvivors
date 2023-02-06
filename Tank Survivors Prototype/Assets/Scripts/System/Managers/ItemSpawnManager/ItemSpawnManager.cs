using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ItemSpawnManager : MonoBehaviour
{
    [SerializeField] private Items items;

    [SerializeField] private int maxItems;

    [SerializeField] private float spawnRate;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;

    [SerializeField] private bool isRandomSpawn;

    Transform player;

    protected ObjectPool<Items> pool;

    int currentItemCount;
    float currentSpawnRate;

    bool spawning = true;

    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;

        currentSpawnRate = spawnRate;

        if (maxItems > 0)
            pool = new ObjectPool<Items>(CreateItem, OnTakeItems, OnReturnItems, maxSize: maxItems);
    }

    private void Update()
    {
        if ((currentItemCount < maxItems && spawning) && isRandomSpawn)
        {
            currentSpawnRate -= Time.deltaTime;
            if (currentSpawnRate <= 0)
            {
                currentSpawnRate = spawnRate;
                GetItem();
            }
        }
    }

    public void DecreaseCurrentCount()
    {
        currentItemCount--;
    }

    public void SetSpawning(bool value) => spawning = value;

    Items CreateItem()
    {
        Items item = Instantiate(items);
        item.SetSpawnManager(this);
        item.SetPool(pool);
        return item;
    }

    void OnTakeItems(Items item)
    {
        currentItemCount++;
        if (isRandomSpawn)
            item.transform.position = (Vector2)player.position + GetRandomPos();
        item.HideObject(false);
    }

    void OnReturnItems(Items item)
    {
        item.HideObject(true);
    }

    void GetItem()
    {
        var item = pool.Get();
    }

    public void GetItem(Vector2 pos)
    {
        var item = pool.Get();
        item.transform.position = pos;
    }

    Vector2 GetRandomPos()
    {
        Vector2 randPos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
        int randValue = Random.Range(-1, 1);
        if (randValue != 0)
            randPos *= randValue;
        return randPos;
    }
}
