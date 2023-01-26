using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class NPCSpawnManager : MonoBehaviour
{
    public static NPCSpawnManager instance;

    /*[SerializeField] private SpawnType type;

    [SerializeField] private NPC spawnObj;

    [SerializeField] private int maxCount;
    [SerializeField] private int minKillingForSpawn;

    [SerializeField] private float spawnRate;
    [SerializeField] private float minSpawnRate;
    [SerializeField] private float maxSpawnRate;

    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;*/

    [SerializeField] private SpawnAliveEntityData spawnData;

    SpawnType type;

    NPC spawnObj;

    int maxCount;
    int minKillingForSpawn;

    float spawnRate;
    float minSpawnRate;
    float maxSpawnRate;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    private Transform player;

    private int currentEnemyCount;

    private void Start()
    {
        instance = this;

        type = spawnData.type;

        spawnObj = spawnData.spawnObj;

        maxCount = spawnData.maxCount;
        minKillingForSpawn = spawnData.minKillingForSpawn;

        spawnRate = spawnData.spawnRate;
        minSpawnRate = spawnData.minSpawnRate;
        maxSpawnRate = spawnData.maxSpawnRate;

        xMin = spawnData.xMin;
        yMin = spawnData.yMin;
        xMax = spawnData.xMax;
        yMax = spawnData.yMax;

        player = FindObjectOfType<Player>().transform;
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            if (currentEnemyCount < maxCount && GameManager.instance.Killings >= minKillingForSpawn)
                Spawn();

            if (type == SpawnType.RandomTime)
            {
                spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
            }
        }
    }

    public void DecreaseCurrentCount()
    {
        currentEnemyCount--;
    }

    void Spawn()
    {
        Vector2 randVec = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
        int randValue = Random.Range(-1, 1);
        if (randValue != 0)
            randVec *= randValue;
        NPC npc = Instantiate(spawnObj, (Vector2)player.position + randVec, Quaternion.identity);
        npc.SetSpawnManager(this);
        currentEnemyCount++;
    }
}

public enum SpawnType
{
    RandomTime,
    ConstTime
}