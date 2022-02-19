using DG.Tweening;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer render;
    [SerializeField] private Color baseColor, offsetColor;

    public string TileName;
    // TODO: 可能多個玩家站在同一格
    public UnitBase OccupiedPlayer;
    public UnitBase OccupiedBomb;
    public int x;
    public int y;

    private float travelTime = 0.2f;

    public virtual void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
        var isOffset = x % 2 != y % 2;
        render.color = isOffset ? offsetColor : baseColor;
    }

    public void SetPlayer(UnitBase unit)
    {
        if (unit.OccupedTile != null)
        {
            unit.OccupedTile.OccupiedPlayer = null;
        }

        unit.transform.DOMove(transform.position, travelTime);
        OccupiedPlayer = unit;
        unit.OccupedTile = this;
    }

    public void SetBomb(UnitBase unit)
    {
        if (unit.OccupedTile != null)
        {
            unit.OccupedTile.OccupiedBomb = null;
        }

        unit.transform.position = transform.position;
        OccupiedBomb = unit;
        unit.OccupedTile = this;
    }

    public void DestroyOccupiedBomb()
    {
        OccupiedBomb = null;
    }
}
