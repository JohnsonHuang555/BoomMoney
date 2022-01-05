using DG.Tweening;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public string TileName;
    [SerializeField] protected SpriteRenderer render;
    [SerializeField] float travelTime = 0.2f;
    //[SerializeField] private bool isWalkable;

    public UnitBase OccupiedUnit;

    public virtual void Init(int x, int y)
    {

    }

    public void SetUnit(UnitBase unit)
    {
        if (unit.OccupedTile != null)
        {
            unit.OccupedTile.OccupiedUnit = null;
        }

        unit.transform.DOMove(transform.position, travelTime);
        OccupiedUnit = unit;
        unit.OccupedTile = this;
    }
}
