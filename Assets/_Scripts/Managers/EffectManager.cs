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
            case Effect.Fire:
                if (Helpers.DoesTagExist("Bomb"))
                {
                    // �����r���K�d��
                    List<GameObject> bombGameObjects = GameObject.FindGameObjectsWithTag("Bomb").ToList();
                    bombGameObjectDict = bombGameObjects.ToDictionary(
                        b => b.GetComponent<Bomb>().position, b => b.GetComponent<Bomb>()
                    );

                    // �C�X�Ҧ��n�z������m
                    Vector2[] bombs = GetAllBombingBomb();

                    // ����z���Ϯ�
                    //foreach (var bomb in bombs)
                    //{
                    //    StartCoroutine(BombExplode(bomb));
                    //}
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
                bombPositions = CheckOtherBombs(bombPositions, bombInstance);
            }
        }
        return bombPositions.ToArray();
    }

    private List<Vector2> CheckOtherBombs(List<Vector2> bombPositions, Bomb bomb)
    {
        // �[�J��e����m
        if (!bombPositions.Contains(bomb.position))
        {
            bombPositions.Add(bomb.position);
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
                    bombPositions = CheckOtherBombs(bombPositions, newBomb);
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
                    bombPositions = CheckOtherBombs(bombPositions, newBomb);
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
                    bombPositions = CheckOtherBombs(bombPositions, newBomb);
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
                    bombPositions = CheckOtherBombs(bombPositions, newBomb);
                }
            }
        }
        return bombPositions;
    }

    // �p��s���z���d������
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
    Fire = 0, // ���O
    Poison = 1 // �r��
}
