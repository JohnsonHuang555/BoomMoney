using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class MapManager : StaticInstance<MapManager>
{
    [SerializeField] Tile normalTile;
    [SerializeField] Transform mainCamera;
    [SerializeField] Vector2 size;
    [SerializeField] Vector2 offset;

    Dictionary<Vector2, Tile> tiles;

    // �g��a�� TODO: ����n�����P��������
    public void GenerateMap()
    {
        var environment = GameObject.FindGameObjectWithTag("Environment");
        // ���a��
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
                }

            }
        }

        mainCamera.transform.position = new Vector3(size.x / 2, size.y / 2, -10);
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
