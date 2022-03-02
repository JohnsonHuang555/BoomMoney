using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DemageCalculateHelper
{
    public static void CalculateBombDemage()
    {
        var bombPositions = EffectManager.Instance.BombingPositions;
        var players = TestData.GetPlayers();

        int totalCount = 0;
        foreach (var position in bombPositions)
        {
            foreach (var player in players)
            {
                Tile tile = MapManager.Instance.GetTileByCharacterName(player.Key);
                if (tile.x == position.x && tile.y == position.y)
                {
                    var occupiedPlayer = (CharacterUnitBase)tile.GetOccupiedPlayer(player.Key);
                    var stats = occupiedPlayer.Stats;

                    // TODO: demage 傷害來自卡片上屬性
                    occupiedPlayer.TakeDamage(30);

                    // 記錄算到第幾個玩家了
                    totalCount++;
                }
            }

            // 代表所有玩家都算完了，後面不需要再繼續跑迴圈
            if (totalCount == players.Count)
            {
                break;
            }
        }
    }
}
