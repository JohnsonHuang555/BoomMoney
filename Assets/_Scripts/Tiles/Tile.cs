using DG.Tweening;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public string TileName;
    [SerializeField] protected SpriteRenderer render;
    [SerializeField] float travelTime = 0.2f;
    //[SerializeField] private bool isWalkable;

    // TODO: 可能多個玩家站在同一格
    public UnitBase OccupiedPlayer;
    public UnitBase OccupiedBomb;

    public virtual void Init(int x, int y)
    {
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
}
