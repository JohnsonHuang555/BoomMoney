using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理所有地圖效果，如火力/毒氣範圍，剩餘回合歸零後Destroy
/// </summary>
public class EffectManager : StaticInstance<EffectManager>
{
    [SerializeField] GameObject explosionPrefab;

    public void SpawnEffect(Effect effect)
    {
        switch (effect)
        {
            case Effect.Fire:
                if (Helpers.DoesTagExist("Bomb"))
                {
                    // 列出所有要爆炸的炸彈
                    Bomb[] bombs = GetAllBombingBomb();
                    foreach (var bomb in bombs)
                    {
                        StartCoroutine(BombExplode(bomb));
                    }
                }
                break;
            case Effect.Poison:
                break;
            default:
                throw new ArgumentOutOfRangeException("Effect not found");
        }
    }

    public Bomb[] GetAllBombingBomb()
    {
        var bombGameObjects = GameObject.FindGameObjectsWithTag("Bomb");
        List<Bomb> bombs = new();
        foreach (var bombGameObject in bombGameObjects)
        {
            var bomb = bombGameObject.GetComponent<Bomb>();
            if (bomb.remainedRound == 0)
            {
                bombs.Add(bomb);
                var positionX = bomb.position.x;
                var positionY = bomb.position.y;
                for (int i = 0; i < bomb.fire; i++)
                {

                }
                var topPosition = new Vector2(x: positionX, y: positionY + 1);
                var rightPosition = new Vector2(x: positionX + 1, y: positionY);
                var bottomPosition = new Vector2(x: positionX, y: positionY - 1);
                var leftPosition = new Vector2(x: positionX - 1, y: positionY);
            }
        }

        return bombs.ToArray();
    }

    // 計算連鎖爆炸範圍並顯示
    private IEnumerator BombExplode(Bomb bomb)
    {
        //var positionX = bomb.position.x;
        //var positionY = bomb.position.y;
        //var currentPosition = MapManager.Instance.GetTileAtPosition(bomb.position);
        //var topPosition = new Vector2(x: positionX, y: positionY + 1);
        //var rightPosition = new Vector2(x: positionX + 1, y: positionY);
        //var bottomPosition = new Vector2(x: positionX, y: positionY - 1);
        //var leftPosition = new Vector2(x: positionX - 1, y: positionY);

        yield return new WaitForSeconds(1);

        //// top
        //Tile topTile = MapManager.Instance.GetTileAtPosition(topPosition);
        //if (topTile && topTile.OccupiedBomb)
        //{
        //    topTile.DestroyOccupiedBomb();
        //    Instantiate(explosionPrefab, topTile.transform.position, Quaternion.identity);
        //}

        //// right
        //Tile rightTile = MapManager.Instance.GetTileAtPosition(rightPosition);
        //if (rightTile && rightTile.OccupiedBomb)
        //{
        //    rightTile.DestroyOccupiedBomb();
        //    Instantiate(explosionPrefab, rightTile.transform.position, Quaternion.identity);
        //}

        //// bottom
        //Tile bottomTile = MapManager.Instance.GetTileAtPosition(bottomPosition);
        //if (bottomTile && bottomTile.OccupiedBomb)
        //{
        //    bottomTile.DestroyOccupiedBomb();
        //    Instantiate(explosionPrefab, bottomTile.transform.position, Quaternion.identity);
        //}

        //// left
        //Tile leftTile = MapManager.Instance.GetTileAtPosition(leftPosition);
        //if (leftTile && leftTile.OccupiedBomb)
        //{
        //    leftTile.DestroyOccupiedBomb();
        //    Instantiate(explosionPrefab, leftTile.transform.position, Quaternion.identity);
        //}

        //// destroy bombs
        //bomb.DestroySelf();
        //Instantiate(explosionPrefab, currentPosition.transform.position, Quaternion.identity);
    }
}

public enum Effect
{
    Fire = 0, // 火力
    Poison = 1 // 毒氣
}
