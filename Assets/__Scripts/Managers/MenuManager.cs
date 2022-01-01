using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private GameObject selectedHeroObject, tileObject, tileUnitObject;

    void Awake()
    {
        Instance = this;
    }

    public void ShowTileInfo(Tile1 tile)
    {
        if (tile == null)
        {
            tileObject.SetActive(false);
            tileUnitObject.SetActive(false);
            return;
        }
        tileObject.GetComponentInChildren<Text>().text = tile.TileName;
        tileObject.SetActive(true);
        if (tile.OccupiedUnit)
        {
            tileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
            tileUnitObject.SetActive(true);
        }
    }

    public void ShowSelectedHero(BaseHero hero)
    {
        if (hero == null)
        {
            selectedHeroObject.SetActive(false);
            return;
        }
        selectedHeroObject.GetComponentInChildren<Text>().text = hero.UnitName;
        selectedHeroObject.SetActive(true);
    }
}
