using UnityEngine;

public abstract class Tile : MonoBehaviour
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
        if (GameManager.Instance.GameState != GameState.HerosTurn)
        {
            return;
        }

        if (OccupiedUnit != null)
        {
            // 選的是英雄就要寫進 SelectedHero
            if (OccupiedUnit.Faction == Faction.Hero)
            {
                UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
            }
            else
            {
                if (UnitManager.Instance.SelectedHero != null)
                {
                    var enemy = (BaseEnemy)OccupiedUnit;
                    Destroy(enemy.gameObject);
                    UnitManager.Instance.SetSelectedHero(null);
                }
            }
        }
        else
        {
            if (UnitManager.Instance.SelectedHero != null)
            {
                SetUnit(UnitManager.Instance.SelectedHero);
                UnitManager.Instance.SetSelectedHero(null);
            }
        }
    }

    public void SetUnit(BaseUnit unit)
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
