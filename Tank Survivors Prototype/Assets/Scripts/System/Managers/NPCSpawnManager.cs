using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Pool;

public class NPCSpawnManager : MonoBehaviour
{
    public static NPCSpawnManager Instance;

    [SerializeField] private SpawnAliveEntityData spawnData;

    private ObjectPool<NPC> pool;

    NPC spawnObj;

    SpawnType type;

    int minKillingForSpawn;

    float spawnRate;
    float currentSpawnRate;

    float spawnRadius;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    int minSpawnCount, maxSpawnCount;

    bool spawnAllOnStart;

    private Transform player;

    private int currentEnemyCount;

    private void Awake()
    {
        Instance = this;

        spawnObj = spawnData.spawnObj;

        type = spawnData.type;

        minKillingForSpawn = spawnData.minKillingForSpawn;

        spawnRate = spawnData.spawnRate;
        currentSpawnRate = spawnData.startSpawnRate;

        spawnRadius = spawnData.spawnRadius;

        xMin = spawnData.xMin;
        yMin = spawnData.yMin;
        xMax = spawnData.xMax;
        yMax = spawnData.yMax;

        minSpawnCount = spawnData.minSpawnCount;
        maxSpawnCount = spawnData.maxSpawnCount;

        spawnAllOnStart = spawnData.spawnAllOnStart;

        pool = new ObjectPool<NPC>(CreateNPC, OnTakeObject, OnReturnObject);
    }

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;

        GameManager.decreaseSpawnRate.AddListener(DecreaseSpawnRate);
    }

    private void Update()
    {
        if (currentEnemyCount < spawnData.maxCount && GameManager.Instance.Killings >= minKillingForSpawn)
        {
            currentSpawnRate -= Time.deltaTime;
            if (currentSpawnRate <= 0)
            {
                currentSpawnRate = spawnRate;

                int count = Random.Range(minSpawnCount, maxSpawnCount);
                if (type == SpawnType.constantCount)
                {
                    count = 1;
                }
                if (spawnAllOnStart)
                {
                    count = spawnData.maxCount;
                    spawnAllOnStart = false;
                }
                Spawn(count);
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

    NPC CreateNPC()
    {
        NPC npc = Instantiate(spawnObj);
        npc.SetSpawnManager(this);
        npc.SetPool(pool);
        return npc;
    }

    void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            NPC npc = GetNPC();
            Vector2 pos;
            if (type == SpawnType.circle)
            {
                int a = 360 / count * i;
                pos = RandCircle(player.position, spawnRadius, a);
            }
            else
            {
                pos = GetRandPos();
            }
            npc.transform.position = pos;
            npc.HideObject(false);
        }
    }

    void OnTakeObject(NPC npc)
    {
        currentEnemyCount++;
        //npc.transform.position = (Vector2)player.position + GetRandPos();
    }

    void OnReturnObject(NPC npc)
    {
        npc.HideObject(true);
    }

    NPC GetNPC()
    {
        return pool.Get();
    }

    Vector2 GetRandPos()
    {
        Vector2 randVec = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
        int randValue = Random.Range(-1, 1);
        if (randValue != 0)
            randVec *= randValue;
        return randVec + (Vector2)player.position;
    }

    Vector2 RandCircle(Vector2 center, float radius, int a)
    {
        float ang = a;
        Vector2 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
}