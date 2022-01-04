using UnityEngine;

public class Tile : MonoBehaviour
{
    public string TileName;
    [SerializeField] protected SpriteRenderer render;
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

        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupedTile = this;
    }
}
