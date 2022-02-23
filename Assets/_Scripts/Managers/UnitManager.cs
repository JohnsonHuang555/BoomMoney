using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 管理單位，生成單位/物件/角色等等
/// </summary>
public class UnitManager : StaticInstance<UnitManager>
{
    public void SpawnPlayers()
    {
        // TODO: from previous scene data
        var players = TestData.GetPlayers();
        foreach (var player in players)
        {
            SpawnPlayer(player.Key);
        }
    }

    /// <summary>
    /// 生成一個玩家
    /// </summary>
    /// <param name="c"></param>
    private void SpawnPlayer(CharacterName c)
    {
        // 取得隨機一格
        var randomTile = MapManager.Instance.GetRandomTile();

        // 取出存在 resource 的角色資料
        var scriptable = ResourceSystem.Instance.GetCharacter(c);

        // 在場景上畫出角色
        var spawnedPlayer = Instantiate(scriptable.Prefab, randomTile.transform.position, Quaternion.identity);

        // 初始化角色數值
        var stats = scriptable.BaseStats;

        // 寫入角色 Unit
        spawnedPlayer.SetStats(stats);

        // 把角色寫入該格子
        randomTile.SetPlayer(spawnedPlayer);
    }

    /// <summary>
    /// 移動角色
    /// </summary>
    /// <returns></returns>
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

    // 逆時針 TODO: 再做一個方法跑順時針
    /// <summary>
    /// 取得所有可能移動的位置
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="dicePoint"></param>
    /// <returns></returns>
    private Vector2[] GetMovePosition(Tile tile, int dicePoint)
    {
        var positions = new List<Vector2>();
        var mapSize = MapManager.Instance.ClassicMapSettings[TestData.WorldSize].tileArea;

        // 當前位置
        var currentPositionX = tile.x;
        var currentPositionY = tile.y;

        if (currentPositionX == 0)
        {
            var totalMoveCount = currentPositionY + dicePoint;
            if (totalMoveCount > mapSize - 1)
            {
                for (int i = 0; i < mapSize - 1 - currentPositionY; i++)
                {
                    positions.Add(new Vector2(x: 0, y: currentPositionY + i + 1));
                }
                var remainPoint = Mathf.Abs(mapSize - 1 - totalMoveCount);
                for (int i = 0; i < remainPoint; i++)
                {
                    positions.Add(new Vector2(x: i + 1, y: mapSize - 1));
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
        else if (currentPositionY == mapSize - 1)
        {
            var totalMoveCount = currentPositionX + dicePoint;
            if (totalMoveCount > mapSize - 1)
            {
                for (int i = 0; i < mapSize - 1 - currentPositionX; i++)
                {
                    positions.Add(new Vector2(x: currentPositionX + i + 1, y: mapSize - 1));
                }
                var remainPoint = Mathf.Abs(mapSize - 1 - totalMoveCount);
                for (int i = 0; i < remainPoint; i++)
                {
                    // 多扣一
                    positions.Add(new Vector2(x: mapSize - 1, y: mapSize - 1 - i - 1));
                }
            }
            else
            {
                for (int i = 0; i < dicePoint; i++)
                {
                    positions.Add(new Vector2(x: currentPositionX + i + 1, y: mapSize - 1));
                }
            }
        }
        else if (currentPositionX == mapSize - 1)
        {
            var movePositionY = currentPositionY - dicePoint;
            if (movePositionY < 0)
            {
                for (int i = 0; i < currentPositionY; i++)
                {
                    positions.Add(new Vector2(x: mapSize - 1, y: currentPositionY - i - 1));
                }
                var remainPoint = Mathf.Abs(currentPositionY - dicePoint);
                for (int i = 0; i < remainPoint; i++)
                {
                    positions.Add(new Vector2(x: mapSize - 1 - i - 1, y: 0));
                }
            }
            else
            {
                for (int i = 0; i < dicePoint; i++)
                {
                    positions.Add(new Vector2(x: mapSize - 1, y: currentPositionY - i - 1));
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

    /// <summary>
    /// 生成炸彈在地圖上
    /// </summary>
    public void SpawnBomb()
    {
        // TODO: 不能寫死
        var tile = MapManager.Instance.GetTileByCharacterName("Fire");
        var scriptable = ResourceSystem.Instance.GetItem(ItemType.Bomb);
        var spawnedItem = Instantiate(scriptable.Prefab, tile.transform.position, Quaternion.identity);

        // Apply possible modifications here such as potion boosts, team synergies, etc
        var stats = scriptable.BaseStats;

        stats.Position = new Vector2(x: tile.x, y: tile.y);
        spawnedItem.SetStats(stats);
        spawnedItem.tag = "Bomb";
        tile.SetBomb(spawnedItem);
    }
}
