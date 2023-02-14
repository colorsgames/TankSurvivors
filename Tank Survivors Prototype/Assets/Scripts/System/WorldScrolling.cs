using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector2Int playerTilePos;
    [SerializeField] float tileSize = 11f;

    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;

    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidth = 3;

    GameObject[,] terrainTiles;

    Vector2Int currentTilePos = new Vector2Int(0, 0);
    Vector2Int onTileGridPlayerPos;

    private void Awake()
    {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }

    private void Update()
    {
        playerTilePos.x = (int)(playerTransform.position.x / tileSize);
        playerTilePos.y = (int)(playerTransform.position.y / tileSize);

        playerTilePos.x -= playerTransform.position.x < 0 ? 1 : 0;
        playerTilePos.y -= playerTransform.position.y < 0 ? 1 : 0;

        if (currentTilePos != playerTilePos)
        {
            currentTilePos = playerTilePos;

            onTileGridPlayerPos.x = CalcilatePositionOnAxis(onTileGridPlayerPos.x, true);
            onTileGridPlayerPos.y = CalcilatePositionOnAxis(onTileGridPlayerPos.y, false);
            UpdateTilesOnScreen();
        }
    }

    void UpdateTilesOnScreen()
    {
        for (int i = -(fieldOfVisionWidth / 2); i <= fieldOfVisionWidth / 2; i++)
        {
            for (int j = -(fieldOfVisionHeight / 2); j <= fieldOfVisionHeight / 2; j++)
            {
                int tileToUpdate_x = CalcilatePositionOnAxis(playerTilePos.x + i, true);
                int tileToUpdate_y = CalcilatePositionOnAxis(playerTilePos.y + j, false);

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                tile.transform.position = CalculatTilePos(playerTilePos.x + i, playerTilePos.y + j);
            }
        }
    }

    Vector3 CalculatTilePos(int x, int y)
    {
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }

    int CalcilatePositionOnAxis(float currentValue, bool horizontal)
    {
        if (horizontal)
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileHorizontalCount;
            }
            else
            {
                currentValue++;
                currentValue = terrainTileHorizontalCount - 1 + currentValue % terrainTileHorizontalCount;
            }
        }
        else
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileVerticalCount;
            }
            else
            {
                currentValue++;
                currentValue = terrainTileVerticalCount - 1 + currentValue % terrainTileVerticalCount;
            }
        }
        return (int)currentValue;
    }

    public void Add(GameObject go, Vector2Int pos)
    {
        terrainTiles[pos.x, pos.y] = go;
    }
}
