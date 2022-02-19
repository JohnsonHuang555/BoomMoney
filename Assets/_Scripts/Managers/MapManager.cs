using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// 管理地圖物件，產不同的格子(Tile)
/// </summary>
public class MapManager : StaticInstance<MapManager>
{
    // 一般格子
    [SerializeField] Tile normalTile;

    // 道場
    [SerializeField] Tile dojoTile;

    // 位移
    [SerializeField] Vector2 offset;

    Dictionary<Vector2, Tile> tiles;
    public Dictionary<WorldSize, Settings> ClassicMapSettings;

    // 經典地圖 TODO: 之後要做不同類型的圖
    public void GenerateMap(Overworld overworld)
    {
        // 初始化地圖設定
        ClassicMapSettings = new();
        ClassicMapSettings.Add(WorldSize.Small, new Settings { tileArea = 5, cameraSize = 5 });

        switch (overworld)
        {
            case Overworld.Classic:
                CreateClassicWorld();
                break;
            default:
                break;
        }
    }

    private void CreateClassicWorld()
    {
        var size = ClassicMapSettings[TestData.WorldSize].tileArea;
        // 塞進 Environment 裡面
        var environment = GameObject.FindGameObjectWithTag("Environment");
        float lastTilePositionY = 0f;
        // 產地圖
        tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < size; x++)
        {
            // 一列隨機產一格放事件
            //var randomEventTile = UnityEngine.Random.Range(0, size);

            for (int y = 0; y < size; y++)
            {
                Tile spawnedTile;
                if (x == 0 || x == size - 1 || y == 0 || y == size - 1)
                {
                    if (y == 3)
                    {
                        spawnedTile = Instantiate(
                            dojoTile,
                            new Vector3((x - y) * offset.x, -(x + y) * offset.y, 1 - (x + y) / (size + size)),
                            Quaternion.identity
                        );
                    }
                    else
                    {
                        spawnedTile = Instantiate(
                            normalTile,
                            new Vector3((x - y) * offset.x, -(x + y) * offset.y, 1 - (x + y) / (size + size)),
                            Quaternion.identity
                        );
                    }
                    spawnedTile.name = $"Tile {x} {y}";
                    spawnedTile.Init(x, y);
                    spawnedTile.transform.SetParent(environment.transform);
                    tiles[new Vector2(x, y)] = spawnedTile;
                    // 設置置中
                    if (x == size - 1 && y == size - 1)
                    {
                        lastTilePositionY = Math.Abs(spawnedTile.transform.position.y) / 2 - 1;
                    }
                }
            }
        }

        environment.transform.position = new Vector3(0, lastTilePositionY + 0.5f, 0);
        Camera.main.orthographicSize = ClassicMapSettings[TestData.WorldSize].cameraSize;
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

// 遊戲地圖
public enum Overworld
{
    Classic,
}

// 地圖大小
public enum WorldSize
{
    Small,
}

public struct Settings
{
    public int tileArea;
    public int cameraSize;
}

public enum MapEvent
{
    Dojo,
}
