using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitManager : StaticInstance<UnitManager>
{
    public void SpawnPlayers()
    {
        // TODO: Multiple players
        //var players = new string[] { "Fire" };

        //foreach (var player in players)
        //{

        //}
        SpawnUnit(CharacterType.Fire);
    }

    private void SpawnUnit(CharacterType c)
    {
        var randomTile = MapManager.Instance.GetRandomTile();
        var fireScriptable = ResourceSystem.Instance.GetCharacter(c);
        var spawnedFire = Instantiate(fireScriptable.Prefab, randomTile.transform.position, Quaternion.identity);

        // Apply possible modifications here such as potion boosts, team synergies, etc
        var stats = fireScriptable.BaseStats;

        spawnedFire.SetStats(stats);
        randomTile.SetUnit(spawnedFire);
    }

    public IEnumerator MovePlayer()
    {
        var fireTile = MapManager.Instance.GetTileByCharacterName("Fire");
        var dicePoint = DiceManager.Instance.dicePoint;
        var positions = GetMovePosition(fireTile, dicePoint);
        var fire = (CharacterUnitBase)fireTile.OccupiedUnit;

        foreach (var position in positions)
        {
            var movedTile = MapManager.Instance.GetTileAtPosition(position);
            yield return new WaitForSeconds(1);
            movedTile.SetUnit(fire);
        }

        yield return new WaitForSeconds(1);
        ConfirmModalManager.Instance.ShowModal(
            new ConfirmModalParams
            {
                description = "�аݭn�񬵼u��?",
                confirmType = ConfirmType.PutBoom
            });
    }

    private Vector2[] GetMovePosition(Tile tile, int dicePoint)
    {
        var positions = new List<Vector2>();

        // ��e��m
        var currentPositionX = tile.transform.position.x;
        var currentPositionY = tile.transform.position.y;

        if (currentPositionX == 0)
        {
            var totalMoveCount = currentPositionY + dicePoint;
            if (totalMoveCount > 7)
            {
                for (int i = 0; i < 7 - currentPositionY; i++)
                {
                    positions.Add(new Vector2(x: 0, y: currentPositionY + i + 1));
                }
                var remainPoint = Mathf.Abs(7 - totalMoveCount);
                for (int i = 0; i < remainPoint; i++)
                {
                    positions.Add(new Vector2(x: i + 1, y: 7));
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
        else if (currentPositionY == 7)
        {
            var totalMoveCount = currentPositionX + dicePoint;
            if (totalMoveCount > 9)
            {
                for (int i = 0; i < 9 - currentPositionX; i++)
                {
                    positions.Add(new Vector2(x: currentPositionX + i + 1, y: 7));
                }
                var remainPoint = Mathf.Abs(9 - totalMoveCount);
                for (int i = 0; i < remainPoint; i++)
                {
                    // �h���@
                    positions.Add(new Vector2(x: 9, y: 7 - i - 1));
                }
            }
            else
            {
                for (int i = 0; i < dicePoint; i++)
                {
                    positions.Add(new Vector2(x: currentPositionX + i + 1, y: 7));
                }
            }
        }
        else if (currentPositionX == 9)
        {
            var movePositionY = currentPositionY - dicePoint;
            if (movePositionY < 0)
            {
                for (int i = 0; i < currentPositionY; i++)
                {
                    positions.Add(new Vector2(x: 9, y: currentPositionY - i - 1));
                }
                var remainPoint = Mathf.Abs(currentPositionY - dicePoint);
                for (int i = 0; i < remainPoint; i++)
                {
                    positions.Add(new Vector2(x: 9 - i - 1, y: 0));
                }
            }
            else
            {
                for (int i = 0; i < dicePoint; i++)
                {
                    positions.Add(new Vector2(x: 9, y: currentPositionY - i - 1));
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
}
