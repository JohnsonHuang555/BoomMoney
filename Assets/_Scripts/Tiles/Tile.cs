using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public string TileName;
    [SerializeField] protected SpriteRenderer render;
    [SerializeField] private bool isWalkable;

    public virtual void Init(int x, int y)
    {

    }
}
