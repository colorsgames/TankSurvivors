using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefabs;

    [SerializeField] private int minItems, maxItems;

    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;

    Vector2 pos;

    private void Start()
    {
        int count = Random.Range(minItems, maxItems);
        Spawn(count);
    }
    
    void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 randPos = new Vector2 (Random.Range(xMin, xMax), Random.Range(yMin, yMax));
            pos = (Vector2)transform.position + randPos;
            Instantiate(itemPrefabs, pos, Quaternion.identity);
        }
    }
}
