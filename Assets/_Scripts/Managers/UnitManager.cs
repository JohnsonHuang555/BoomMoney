using UnityEngine;
using System.Collections;

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
        var currentPlayer = GameManager.Instance.CurrentPlayer;
        var fireTile = MapManager.Instance.GetTileByCharacterName(currentPlayer);
        var positions = MovePosition.GetMovePosition(fireTile, DiceManager.Instance.dicePoint);
        var fire = (CharacterUnitBase)fireTile.GetOccupiedPlayer(currentPlayer);
        fireTile.DeleteOccupiedPlayer(currentPlayer);

        Tile previousTile = null;
        foreach (var position in positions)
        {
            yield return new WaitForSeconds(1);
            var movedTile = MapManager.Instance.GetTileAtPosition(position);
            // 移除前一個位置的玩家
            if (previousTile)
            {
                previousTile.DeleteOccupiedPlayer(currentPlayer);
            }
            Debug.Log(movedTile);
            movedTile.SetPlayer(fire);
            previousTile = movedTile;
        }

        yield return new WaitForSeconds(1);
        GameManager.Instance.ChangeState(GameState.BombExplode);
    }

    /// <summary>
    /// 生成炸彈在地圖上
    /// </summary>
    public void SpawnBomb()
    {
        var tile = MapManager.Instance.GetTileByCharacterName(GameManager.Instance.CurrentPlayer);
        var scriptable = ResourceSystem.Instance.GetItem(ItemType.Bomb);
        var spawnedItem = Instantiate(scriptable.Prefab, tile.transform.position, Quaternion.identity);

        var stats = scriptable.BaseStats;
        stats.Position = new Vector2(x: tile.x, y: tile.y);

        spawnedItem.SetStats(stats);
        spawnedItem.tag = "Bomb";
        tile.SetBomb(spawnedItem);
    }
}
