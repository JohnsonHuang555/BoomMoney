using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// �޲z�a�Ϫ���A�����P����l(Tile)
/// </summary>
public class MapManager : StaticInstance<MapManager>
{
    // �@���l
    [SerializeField] Tile normalTile;

    // �D��
    [SerializeField] Tile dojoTile;

    // �첾
    [SerializeField] Vector2 offset;

    Dictionary<Vector2, Tile> tiles;
    public Dictionary<WorldSize, Settings> ClassicMapSettings;

    // �g��a�� TODO: ����n�����P��������
    public void GenerateMap(Overworld overworld)
    {
        // ��l�Ʀa�ϳ]�w
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
        // ��i Environment �̭�
        var environment = GameObject.FindGameObjectWithTag("Environment");
        float lastTilePositionY = 0f;
        // ���a��
        tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < size; x++)
        {
            // �@�C�H�����@���ƥ�
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
                    // �]�m�m��
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
    /// �̷Ӧ�m�^�Ǯ�l
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

    /// <summary>
    /// ��쨤��b���@��
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Tile GetTileByCharacterName(string name)
    {
        return tiles.Where(t => t.Value.OccupiedPlayer && t.Value.OccupiedPlayer.UnitName == name).First().Value;
    }
}

// �C���a��
public enum Overworld
{
    Classic,
}

// �a�Ϥj�p
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
