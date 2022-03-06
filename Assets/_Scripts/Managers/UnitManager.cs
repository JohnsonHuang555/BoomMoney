using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// �޲z���A�ͦ����/����/���ⵥ��
/// </summary>
public class UnitManager : StaticInstance<UnitManager>
{
    /// <summary>
    /// ���o�Ҧ����a�C������
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetGameObjects(string tagName)
    {
        return GameObject.FindGameObjectsWithTag(tagName).ToList();
    }

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
    /// �ͦ��@�Ӫ��a
    /// </summary>
    /// <param name="c"></param>
    private void SpawnPlayer(CharacterName c)
    {
        // ���o�H���@��
        var randomTile = MapManager.Instance.GetRandomTile();

        // ���X�s�b resource ��������
        var scriptable = ResourceSystem.Instance.GetCharacter(c);

        // �b�����W�e�X����
        var spawnedPlayer = Instantiate(scriptable.Prefab, randomTile.transform.position, Quaternion.identity);

        // ��l�ƨ���ƭ�
        var stats = scriptable.BaseStats;

        // �g�J���� Unit
        spawnedPlayer.SetStats(stats);
        spawnedPlayer.tag = "Character";

        // �⨤��g�J�Ӯ�l
        randomTile.SetPlayer(spawnedPlayer);
    }

    /// <summary>
    /// ���ʨ���
    /// </summary>
    /// <returns></returns>
    public IEnumerator MovePlayer()
    {
        var currentPlayer = GameManager.Instance.CurrentPlayer;
        var currentPlayerTile = MapManager.Instance.GetTileByCharacterName(currentPlayer);
        var positions = MovePositionHelper.GetMovePosition(currentPlayerTile, DiceManager.Instance.dicePoint);
        var occupiedPlayer = (CharacterUnitBase)currentPlayerTile.GetOccupiedPlayer(currentPlayer);
        currentPlayerTile.DeleteOccupiedPlayer(currentPlayer);

        Tile previousTile = null;
        foreach (var position in positions)
        {
            yield return new WaitForSeconds(1);
            var movedTile = MapManager.Instance.GetTileAtPosition(position);
            // �����e�@�Ӧ�m�����a
            if (previousTile)
            {
                previousTile.DeleteOccupiedPlayer(currentPlayer);
            }
            Debug.Log(movedTile);
            movedTile.SetPlayer(occupiedPlayer);
            previousTile = movedTile;
        }

        yield return new WaitForSeconds(1);

        // �g�J�s���a
        var newCurrentPlayer = PlayerHelper.GetNewCurrentPlayer(GameManager.Instance.CurrentPlayer);
        GameManager.Instance.SetNewCurrentPlayer(newCurrentPlayer);
        GameManager.Instance.ChangeState(GameState.BombExplode);
    }

    /// <summary>
    /// �ͦ����u�b�a�ϤW
    /// </summary>
    public void SpawnBomb()
    {
        var tile = MapManager.Instance.GetTileByCharacterName(GameManager.Instance.CurrentPlayer);
        // �w�g�����u�������
        if (tile.OccupiedBomb != null)
        {
            return;
        }

        var scriptable = ResourceSystem.Instance.GetItem(ItemType.Bomb);
        var spawnedItem = Instantiate(scriptable.Prefab, tile.transform.position, Quaternion.identity);

        var stats = scriptable.BaseStats;
        stats.Position = new Vector2(x: tile.x, y: tile.y);

        spawnedItem.SetStats(stats);
        spawnedItem.tag = "Bomb";
        tile.SetBomb(spawnedItem);
    }
}
