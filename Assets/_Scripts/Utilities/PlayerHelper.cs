using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �B�z���a�����޿�
/// </summary>
public static class PlayerHelper
{
    /// <summary>
    /// ���o�U�@�쪱�a
    /// </summary>
    /// <param name="currentPlayer"></param>
    /// <returns></returns>
    public static CharacterName GetNewCurrentPlayer(CharacterName currentPlayer)
    {
        var newPlayer = currentPlayer;
        // ���Ҧ����a
        var nowPlayers = TestData.GetPlayers();

        // ��e���a playerOrder
        var currentPlayerOrder = nowPlayers[currentPlayer].PlayOrder;

        // �U�@�Ӫ��a playerOrder
        var nextPlayerOrder = currentPlayerOrder + 1;

        // �ˬd�O�_�^��Ĥ@�Ӫ��a�A�Y playerOrder = 0
        if (nextPlayerOrder == nowPlayers.Count)
        {
            nextPlayerOrder = 0;
        }

        foreach (var player in nowPlayers)
        {
            if (player.Value.PlayOrder == nextPlayerOrder)
            {
                // �s���a����
                newPlayer = player.Key;
                break;
            }
        }

        return newPlayer;
    }
}
