using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] Tile grassTile, mountainTile;
    [SerializeField] Transform camera;

    int width = 10, height = 8;
    Dictionary<Vector2, Tile> tiles;

    void Awake()
    {
        Instance = this;
    }

    public void GenerateGrid()
    {
        // ²£¦a¹Ï
        tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile spawnedTile;
                if (x == 0 || x == 9 || y == 0 || y == 7)
                {
                    spawnedTile = Instantiate(grassTile, new Vector3(x, y), Quaternion.identity);
                }
                else
                {
                    spawnedTile = Instantiate(mountainTile, new Vector3(x, y), Quaternion.identity);
                }

                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.Init(x, y);

                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        camera.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);

        GameManager.Instance.ChangeState(GameState.SpawnHeros);
    }

    public Tile GetHeroSpawnTile()
    {
        return tiles.Where(t => t.Key.x < width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetEnemySpawnTile()
    {
        return tiles.Where(t => t.Key.x > width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

    public Tile GetTileByName(string name)
    {
        return tiles.Where(t => t.Value.OccupiedUnit.UnitName == name).First().Value;
    }
}
