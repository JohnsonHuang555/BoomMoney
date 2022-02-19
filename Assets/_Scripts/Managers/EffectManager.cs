using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// �޲z�Ҧ��a�ϮĪG�A�p���O/�r��d��A�Ѿl�^�X�k�s��Destroy
/// </summary>
public class EffectManager : StaticInstance<EffectManager>
{
    [SerializeField] GameObject explosionPrefab;

    private Dictionary<Vector2, Bomb> bombGameObjectDict;

    public void SpawnEffect(Effect effect)
    {
        switch (effect)
        {
            case Effect.Explosion:
                if (Helpers.DoesTagExist("Bomb"))
                {
                    // �����r���K�d��
                    List<GameObject> bombGameObjects = GameObject.FindGameObjectsWithTag("Bomb").ToList();
                    bombGameObjectDict = bombGameObjects.ToDictionary(
                        b => b.GetComponent<Bomb>().position, b => b.GetComponent<Bomb>()
                    );

                    // �C�X�Ҧ��n�z������m
                    Vector2[] bombPositions = GetAllBombingBomb();

                    // ����z���Ϯ�
                    StartCoroutine(ShowExplodeEffect(bombPositions));
                }
                break;
            case Effect.Poison:
                break;
            default:
                throw new ArgumentOutOfRangeException("Effect not found");
        }
    }

    public Vector2[] GetAllBombingBomb()
    {
        // �Ҧ��|�z������l�y��
        List<Vector2> bombPositions = new();

        // �v�@�ˬd�O�_�z��
        foreach (var bomb in bombGameObjectDict)
        {
            Bomb bombInstance = bomb.Value;
            if (bombInstance.remainedRound == 0)
            {
                bombPositions = CheckBombsStats(bombPositions, bombInstance);
            }
        }
        return bombPositions.ToArray();
    }

    private List<Vector2> CheckBombsStats(List<Vector2> bombPositions, Bomb bomb)
    {
        // �[�J��e����m
        if (!bombPositions.Contains(bomb.position))
        {
            bombPositions.Add(bomb.position);
            bomb.DestroySelf();
        }

        // �ھڤ��O�P�_��l���L���u
        for (int i = 0; i < bomb.fire; i++)
        {
            var topPosition = new Vector2(x: bomb.position.x, y: bomb.position.y + i + 1);
            var rightPosition = new Vector2(x: bomb.position.x + i + 1, y: bomb.position.y);
            var bottomPosition = new Vector2(x: bomb.position.x, y: bomb.position.y - i - 1);
            var leftPosition = new Vector2(x: bomb.position.x - i - 1, y: bomb.position.y);

            // top
            Tile topTile = MapManager.Instance.GetTileAtPosition(topPosition);
            if (topTile)
            {
                Vector2 position = new(x: topTile.x, y: topTile.y);
                if (!bombPositions.Contains(position))
                {
                    bombPositions.Add(position);
                }
                if (topTile.OccupiedBomb)
                {
                    topTile.DestroyOccupiedBomb();
                    Bomb newBomb = bombGameObjectDict[position];
                    bombPositions = CheckBombsStats(bombPositions, newBomb);
                    newBomb.DestroySelf();
                }
            }

            // right
            Tile rightTile = MapManager.Instance.GetTileAtPosition(rightPosition);
            if (rightTile)
            {
                Vector2 position = new(x: rightTile.x, y: rightTile.y);
                if (!bombPositions.Contains(position))
                {
                    bombPositions.Add(position);
                }
                if (rightTile.OccupiedBomb)
                {
                    rightTile.DestroyOccupiedBomb();
                    Bomb newBomb = bombGameObjectDict[position];
                    bombPositions = CheckBombsStats(bombPositions, newBomb);
                    newBomb.DestroySelf();
                }
            }

            // bottom
            Tile bottomTile = MapManager.Instance.GetTileAtPosition(bottomPosition);
            if (bottomTile)
            {
                Vector2 position = new(x: bottomTile.x, y: bottomTile.y);
                if (!bombPositions.Contains(position))
                {
                    bombPositions.Add(position);
                }
                if (bottomTile.OccupiedBomb)
                {
                    bottomTile.DestroyOccupiedBomb();
                    Bomb newBomb = bombGameObjectDict[position];
                    bombPositions = CheckBombsStats(bombPositions, newBomb);
                    newBomb.DestroySelf();
                }
            }

            // left
            Tile leftTile = MapManager.Instance.GetTileAtPosition(leftPosition);
            if (leftTile)
            {
                Vector2 position = new(x: leftTile.x, y: leftTile.y);
                if (!bombPositions.Contains(position))
                {
                    bombPositions.Add(position);
                }
                if (leftTile.OccupiedBomb)
                {
                    leftTile.DestroyOccupiedBomb();
                    Bomb newBomb = bombGameObjectDict[position];
                    bombPositions = CheckBombsStats(bombPositions, newBomb);
                    newBomb.DestroySelf();
                }
            }
        }
        return bombPositions;
    }

    // �p��s���z���d������
    private IEnumerator ShowExplodeEffect(Vector2[] explodePositions)
    {
        yield return new WaitForSeconds(1);
        foreach (var position in explodePositions)
        {
            // ���Ӯ� GameObject ��m
            var tilePosition = MapManager.Instance.GetTileAtPosition(position);
            Instantiate(explosionPrefab, tilePosition.transform.position, Quaternion.identity);
        }
    }
}

public enum Effect
{
    Explosion = 0, // ���O
    Poison = 1 // �r��
}
