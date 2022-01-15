using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
                //Tile spawnedTile;
                //if (x == 0 || x == 9 || y == 0 || y == 7)
                //{
                Instantiate(normalTile, new Vector3(x, y), Quaternion.identity);

                //    spawnedTile.name = $"Tile {x} {y}";
                //    spawnedTile.Init(x, y);
                //    tiles[new Vector2(x, y)] = spawnedTile;
                //}

            }
        }

        mainCamera.transform.position = new Vector3((float)width / 2, (float)height / 2, -10);
    }

    public Tile GetRandomTile()
    {
        return tiles.OrderBy(t => Random.value).First().Value;
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

    public Tile GetTileByCharacterName(string name)
    {
        return tiles.Where(t => t.Value.OccupiedPlayer && t.Value.OccupiedPlayer.UnitName == name).First().Value;
    }
}
