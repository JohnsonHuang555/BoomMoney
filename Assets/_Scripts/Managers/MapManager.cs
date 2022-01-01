using System.Collections.Generic;
using UnityEngine;

public class MapManager : StaticInstance<MapManager>
{
    [SerializeField] Tile normalTile;
    [SerializeField] Transform mainCamera;

    // TODO: 大小要可以調整
    int width = 10, height = 8;
    Dictionary<Vector2, Tile> tiles;

    public void GenerateMap()
    {
        // 產地圖
        tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile spawnedTile;
                if (x == 0 || x == 9 || y == 0 || y == 7)
                {
                    spawnedTile = Instantiate(normalTile, new Vector3(x, y), Quaternion.identity);

                    spawnedTile.name = $"Tile {x} {y}";
                    spawnedTile.Init(x, y);
                    tiles[new Vector2(x, y)] = spawnedTile;
                }

            }
        }

        mainCamera.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }
}
