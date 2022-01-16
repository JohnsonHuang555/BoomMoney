using UnityEngine;

public class NormalTile : Tile
{
    [SerializeField] private Color baseColor, offsetColor;
    public override void Init(int x, int y)
    {
        //var isOffset = x % 2 != y % 2;
        //render.color = isOffset ? offsetColor : baseColor;
    }
}
