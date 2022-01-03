using UnityEngine;

public abstract class Tile1 : MonoBehaviour
{
    public string TileName;
    [SerializeField] protected SpriteRenderer render;
    [SerializeField] private GameObject highlight;
    [SerializeField] private bool isWalkable;

    public BaseUnit OccupiedUnit;
    public bool Walkable => isWalkable && OccupiedUnit == null;

    public virtual void Init(int x, int y)
    {

    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
    }

    private void OnMouseDown()
    {
        //if (GameManager1.Instance.GameState != __GameState.HerosTurn)
        //{
        //    return;
        //}

        if (OccupiedUnit != null)
        {
            // 選的是英雄就要寫進 SelectedHero
            //if (OccupiedUnit.Faction == Faction.Heroes)
            //{
            //    UnitManager1.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
            //}
            //else
            //{
            //    if (UnitManager1.Instance.SelectedHero != null)
            //    {
            //        var enemy = (BaseEnemy)OccupiedUnit;
            //        Destroy(enemy.gameObject);
            //        UnitManager1.Instance.SetSelectedHero(null);
            //    }
            //}
        }
        else
        {
            if (UnitManager1.Instance.SelectedHero != null)
            {
                SetUnit(UnitManager1.Instance.SelectedHero);
                UnitManager1.Instance.SetSelectedHero(null);
            }
        }
    }

    public void SetUnit(BaseUnit unit)
    {
        //if (unit.OccupedTile != null)
        //{
        //    unit.OccupedTile.OccupiedUnit = null;
        //}

        //unit.transform.position = transform.position;
        //OccupiedUnit = unit;
        //unit.OccupedTile = this;
    }
}
