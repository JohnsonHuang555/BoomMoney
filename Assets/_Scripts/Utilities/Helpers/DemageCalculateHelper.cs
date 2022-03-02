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

                    // TODO: demage �ˮ`�Ӧۥd���W�ݩ�
                    occupiedPlayer.TakeDamage(30);

                    // �O�����ĴX�Ӫ��a�F
                    totalCount++;
                }
            }

            // �N��Ҧ����a���⧹�F�A�᭱���ݭn�A�~��]�j��
            if (totalCount == players.Count)
            {
                break;
            }
        }
    }
}
