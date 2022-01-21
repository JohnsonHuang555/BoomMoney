using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// 管理地圖物件，產不同的格子(Tile)
/// </summary>
public class MapManager : StaticInstance<MapManager>
{
    [SerializeField] Tile normalTile;
    [SerializeField] Transform mainCamera;
    [SerializeField] public Vector2 size;
    [SerializeField] Vector2 offset;

    Dictionary<Vector2, Tile> tiles;

    // 經典地圖 TODO: 之後要做不同類型的圖
    public void GenerateMap()
    {
        // 塞進 Environment 裡面
        var environment = GameObject.FindGameObjectWithTag("Environment");
        float lastTilePositionY = 0f;
        // 產地圖
        tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Tile spawnedTile;
                if (x == 0 || x == size.x - 1 || y == 0 || y == size.y - 1)
                {
                    spawnedTile = Instantiate(
                        normalTile,
                        new Vector3((x - y) * offset.x, -(x + y) * offset.y, 1 - (x + y) / (size.x + size.y)),
                        Quaternion.identity
                    );
                    spawnedTile.name = $"Tile {x} {y}";
                    spawnedTile.Init(x, y);
                    spawnedTile.transform.SetParent(environment.transform);
                    tiles[new Vector2(x, y)] = spawnedTile;
                    // 設置置中
                    if (x == size.x - 1 && y == size.y - 1)
                    {
                        lastTilePositionY = Math.Abs(spawnedTile.transform.position.y) / 2 - 1;
                    }
                }
            }
        }

        environment.transform.position = new Vector3(0, lastTilePositionY, 0);
        Camera.main.orthographicSize = (float)MapCameraSize.Small;
    }

    public Tile GetRandomTile()
    {
        return tiles.OrderBy(t => UnityEngine.Random.value).First().Value;
    }

    /// <summary>
    /// 依照位置回傳格子
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

    /// <summary>
    /// 找到角色在哪一格
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Tile GetTileByCharacterName(string name)
    {
        return tiles.Where(t => t.Value.OccupiedPlayer && t.Value.OccupiedPlayer.UnitName == name).First().Value;
    }
}

public enum MapCameraSize
{
    Small = 8,
    Medium = 10,
    Large = 12,
}
