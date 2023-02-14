using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    [SerializeField] Vector2Int tilePos;

    private void Start()
    {
        GetComponentInParent<WorldScrolling>().Add(gameObject, tilePos);
    }
}
