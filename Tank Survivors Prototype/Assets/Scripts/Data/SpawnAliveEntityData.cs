using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Spawn Alive Entity Data", menuName = "Spawn Alive Entity Data", order = 64)]
public class SpawnAliveEntityData : ScriptableObject
{
    public SpawnType type;

    public NPC spawnObj;

    public int maxCount;
    public int minKillingForSpawn;

    public float spawnRate;
    public float minSpawnRate;
    public float maxSpawnRate;

    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
}
