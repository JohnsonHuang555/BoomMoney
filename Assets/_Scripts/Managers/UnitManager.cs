using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// �޲z���A�ͦ����/����/���ⵥ��
/// </summary>
public class UnitManager : StaticInstance<UnitManager>
{
    public void SpawnPlayers()
    {
        // TODO: Multiple players
        //var players = new string[] { "Fire" };

        //foreach (var player in players)
        //{

        //}
        SpawnPlayer(CharacterType.Fire);
    }

    private void SpawnPlayer(CharacterType c)
    {
        var randomTile = MapManager.Instance.GetRandomTile();
        var scriptable = ResourceSystem.Instance.GetCharacter(c);
        var spawnedPlayer = Instantiate(scriptable.Prefab, randomTile.transform.position, Quaternion.identity);

        // Apply possible modifications here such as potion boosts, team synergies, etc
        var stats = scriptable.BaseStats;

        spawnedPlayer.SetStats(stats);
        randomTile.SetPlayer(spawnedPlayer);
    }

    public IEnumerator MovePlayer()
    {
        var fireTile = MapManager.Instance.GetTileByCharacterName("Fire");
        var dicePoint = DiceManager.Instance.dicePoint;
        var positions = GetMovePosition(fireTile, dicePoint);
        var fire = (CharacterUnitBase)fireTile.OccupiedPlayer;

        foreach (var position in positions)
        {
            var movedTile = MapManager.Instance.GetTileAtPosition(position);
            yield return new WaitForSeconds(1);
            movedTile.SetPlayer(fire);
        }

        yield return new WaitForSeconds(1);
        GameManager.Instance.ChangeState(GameState.BombExplode);
    }

    // �f�ɰw TODO: �A���@�Ӥ�k�]���ɰw
    private Vector2[] GetMovePosition(Tile tile, int dicePoint)
    {
        var positions = new List<Vector2>();
        var mapSize = MapManager.Instance.size;

        // ��e��m
        var currentPositionX = tile.x;
        var currentPositionY = tile.y;

        if (currentPositionX == 0)
        {
            var totalMoveCount = currentPositionY + dicePoint;
            if (totalMoveCount > mapSize.y - 1)
            {
                for (int i = 0; i < mapSize.y - 1 - currentPositionY; i++)
                {
                    positions.Add(new Vector2(x: 0, y: currentPositionY + i + 1));
                }
                var remainPoint = Mathf.Abs(mapSize.y - 1 - totalMoveCount);
                for (int i = 0; i < remainPoint; i++)
                {
                    positions.Add(new Vector2(x: i + 1, y: mapSize.y - 1));
                }
            }
            else
            {
                for (int i = 0; i < dicePoint; i++)
                {
                    positions.Add(new Vector2(x: 0, y: currentPositionY + i + 1));
                }
            }
        }
        else if (currentPositionY == mapSize.y - 1)
        {
            var totalMoveCount = currentPositionX + dicePoint;
            if (totalMoveCount > mapSize.x - 1)
            {
                for (int i = 0; i < mapSize.x - 1 - currentPositionX; i++)
                {
                    positions.Add(new Vector2(x: currentPositionX + i + 1, y: mapSize.y - 1));
                }
                var remainPoint = Mathf.Abs(mapSize.x - 1 - totalMoveCount);
                for (int i = 0; i < remainPoint; i++)
                {
                    // �h���@
                    positions.Add(new Vector2(x: mapSize.x - 1, y: mapSize.y - 1 - i - 1));
                }
            }
            else
            {
                for (int i = 0; i < dicePoint; i++)
                {
                    positions.Add(new Vector2(x: currentPositionX + i + 1, y: mapSize.y - 1));
                }
            }
        }
        else if (currentPositionX == mapSize.x - 1)
        {
            var movePositionY = currentPositionY - dicePoint;
            if (movePositionY < 0)
            {
                for (int i = 0; i < currentPositionY; i++)
                {
                    positions.Add(new Vector2(x: mapSize.x - 1, y: currentPositionY - i - 1));
                }
                var remainPoint = Mathf.Abs(currentPositionY - dicePoint);
                for (int i = 0; i < remainPoint; i++)
                {
                    positions.Add(new Vector2(x: mapSize.x - 1 - i - 1, y: 0));
                }
            }
            else
            {
                for (int i = 0; i < dicePoint; i++)
                {
                    positions.Add(new Vector2(x: mapSize.x - 1, y: currentPositionY - i - 1));
                }
            }
        }
        else if (currentPositionY == 0)
        {
            var movePositionX = currentPositionX - dicePoint;
            if (movePositionX < 0)
            {
                for (int i = 0; i < currentPositionX; i++)
                {
                    positions.Add(new Vector2(x: currentPositionX - i - 1, y: 0));
                }
                var remainPoint = Mathf.Abs(currentPositionX - dicePoint);
                for (int i = 0; i < remainPoint; i++)
                {
                    positions.Add(new Vector2(x: 0, y: i + 1));
                }
            }
            else
            {
                for (int i = 0; i < dicePoint; i++)
                {
                    positions.Add(new Vector2(x: currentPositionX - i - 1, y: 0));
                }
            }
        }

        return positions.ToArray();
    }

    public void SetBomb()
    {
        // TODO: ����g��
        var tile = MapManager.Instance.GetTileByCharacterName("Fire");
        var scriptable = ResourceSystem.Instance.GetItem(ItemType.Bomb);
        var spawnedItem = Instantiate(scriptable.Prefab, tile.transform.position, Quaternion.identity);

        // Apply possible modifications here such as potion boosts, team synergies, etc
        var stats = scriptable.BaseStats;

        // FIXME: ���Ӷ��e���a�����O
        stats.Fire = 0;
        stats.Position = new Vector2(x: tile.x, y: tile.y);
        spawnedItem.SetStats(stats);
        spawnedItem.tag = "Bomb";
        tile.SetBomb(spawnedItem);
    }
}
