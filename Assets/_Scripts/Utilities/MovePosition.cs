using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 處理移動邏輯
/// </summary>
public static class MovePosition
{
    // 逆時針 TODO: 再做一個方法跑順時針
    /// <summary>
    /// 取得所有可能移動的位置
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="dicePoint"></param>
    /// <returns></returns>
    public static Vector2[] GetMovePosition(Tile tile, int dicePoint)
    {
        var positions = new List<Vector2>();
        var mapSize = MapManager.Instance.ClassicMapSettings[TestData.WorldSize].tileArea;

        // 當前位置
        var currentPositionX = tile.x;
        var currentPositionY = tile.y;

        // 最大位置
        var maxPositionX = mapSize - 1;
        var maxPositionY = mapSize - 1;

        if (currentPositionX == 0 && currentPositionY < maxPositionY)
        {
            var totalMoveCount = currentPositionY + dicePoint;
            if (totalMoveCount > maxPositionY)
            {
                for (int i = 0; i < maxPositionY - currentPositionY; i++)
                {
                    positions.Add(new Vector2(x: 0, y: currentPositionY + i + 1));
                }
                var remainPoint = Mathf.Abs(maxPositionY - totalMoveCount);
                for (int i = 0; i < remainPoint; i++)
                {
                    positions.Add(new Vector2(x: i + 1, y: maxPositionY));
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
        else if (currentPositionY == maxPositionY)
        {
            var totalMoveCount = currentPositionX + dicePoint;
            if (totalMoveCount > maxPositionX)
            {
                for (int i = 0; i < maxPositionX - currentPositionX; i++)
                {
                    positions.Add(new Vector2(x: currentPositionX + i + 1, y: maxPositionY));
                }
                var remainPoint = Mathf.Abs(maxPositionX - totalMoveCount);
                for (int i = 0; i < remainPoint; i++)
                {
                    positions.Add(new Vector2(x: maxPositionX, y: maxPositionY - i - 1));
                }
            }
            else
            {
                for (int i = 0; i < dicePoint; i++)
                {
                    positions.Add(new Vector2(x: currentPositionX + i + 1, y: maxPositionY));
                }
            }
        }
        else if (currentPositionX == maxPositionX)
        {
            var movePositionY = currentPositionY - dicePoint;
            if (movePositionY < 0)
            {
                for (int i = 0; i < currentPositionY; i++)
                {
                    positions.Add(new Vector2(x: maxPositionX, y: currentPositionY - i - 1));
                }
                var remainPoint = Mathf.Abs(currentPositionY - dicePoint);
                for (int i = 0; i < remainPoint; i++)
                {
                    positions.Add(new Vector2(x: maxPositionX - i - 1, y: 0));
                }
            }
            else
            {
                for (int i = 0; i < dicePoint; i++)
                {
                    positions.Add(new Vector2(x: maxPositionX, y: currentPositionY - i - 1));
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
