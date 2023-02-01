using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Pool;

public class NPCSpawnManager : MonoBehaviour
{
    public static NPCSpawnManager instance;

    [SerializeField] private SpawnAliveEntityData spawnData;

    private ObjectPool<NPC> pool;

    NPC spawnObj;

    int maxCount;
    int minKillingForSpawn;

    float spawnRate;
    float currentSpawnRate;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    private Transform player;

    private int currentEnemyCount;

    private void Awake()
    {
        instance = this;

        spawnObj = spawnData.spawnObj;

        maxCount = spawnData.maxCount;
        minKillingForSpawn = spawnData.minKillingForSpawn;

        spawnRate = spawnData.spawnRate;
        currentSpawnRate = spawnData.spawnRate;

        xMin = spawnData.xMin;
        yMin = spawnData.yMin;
        xMax = spawnData.xMax;
        yMax = spawnData.yMax;

        if (maxCount > 0)
            pool = new ObjectPool<NPC>(Spawn, OnTakeObject, OnReturnObject, maxSize: maxCount);
    }

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;

        GameManager.decreaseSpawnRate.AddListener(DecreaseSpawnRate);
    }

    private void Update()
    {
        if (currentEnemyCount < maxCount && GameManager.instance.Killings >= minKillingForSpawn)
        {
            currentSpawnRate -= Time.deltaTime;
            if (currentSpawnRate <= 0)
            {
                currentSpawnRate = spawnRate;
                GetNPC();
            }
        }
    }

    public void DecreaseCurrentCount()
    {
        currentEnemyCount--;
    }

    public void DecreaseSpawnRate()
    {
        if (spawnObj.GetComponent<Ally>()) return;
        spawnRate = spawnRate / 2;
    }

    NPC Spawn()
    {
        NPC npc = Instantiate(spawnObj);
        npc.SetSpawnManager(this);
        npc.SetPool(pool);
        return npc;
    }

    void OnTakeObject(NPC npc)
    {
        currentEnemyCount++;
        npc.transform.position = (Vector2)player.position + GetRandPos();
        npc.HideObject(false);
    }

    void OnReturnObject(NPC npc)
    {
        npc.HideObject(true);
    }

    void GetNPC()
    {
        var npc = pool.Get();
    }

    Vector2 GetRandPos()
    {
        Vector2 randVec = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
        int randValue = Random.Range(-1, 1);
        if (randValue != 0)
            randVec *= randValue;
        return randVec;
    }
}