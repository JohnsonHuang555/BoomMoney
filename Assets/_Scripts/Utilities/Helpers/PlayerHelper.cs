using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 處理玩家相關邏輯
/// </summary>
public static class PlayerHelper
{
    /// <summary>
    /// 取得下一位玩家
    /// </summary>
    /// <param name="currentPlayer"></param>
    /// <returns></returns>
    public static CharacterName GetNewCurrentPlayer(CharacterName currentPlayer)
    {
        var newPlayer = currentPlayer;
        // 取所有玩家
        var nowPlayers = TestData.GetPlayers();

        // 當前玩家 playerOrder
        var currentPlayerOrder = nowPlayers[currentPlayer].PlayOrder;

        // 下一個玩家 playerOrder
        var nextPlayerOrder = currentPlayerOrder + 1;

        // 檢查是否回到第一個玩家，即 playerOrder = 0
        if (nextPlayerOrder == nowPlayers.Count)
        {
            nextPlayerOrder = 0;
        }

        foreach (var player in nowPlayers)
        {
            if (player.Value.PlayOrder == nextPlayerOrder)
            {
                // 新玩家產生
                newPlayer = player.Key;
                break;
            }
        }

        return newPlayer;
    }
}
