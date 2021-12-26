using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    public BaseHero SelectedHero;
    List<ScriptableUnit> units;

    private void Awake()
    {
        Instance = this;

        units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnHeros()
    {
        var heros = new string[] {"Fire"};

        foreach (var hero in heros)
        {
            var firePrefab = GetUnitByName<BaseHero>(Faction.Hero, hero);
            var spawnedHero = Instantiate(firePrefab);
            var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();

            randomSpawnTile.SetUnit(spawnedHero);
        }

        DiceManager.Instance.ShowDiceButton();
        GameManager.Instance.ChangeState(GameState.HerosTurn);
    }

    /*
    public void SpawnEnemy()
    {
        // TODO;
        var enemyCount = 1;

        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();

            randomSpawnTile.SetUnit(spawnedEnemy);
        }

        GameManager.Instance.ChangeState(GameState.HerosTurn);
    }
    */

    private T GetUnitByName<T>(Faction faction, string name) where T : BaseUnit
    {
        return (T)units.Where(u => u.Faction == faction && u.UnitPrefab.UnitName == name).First().UnitPrefab;
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }

    public void SetSelectedHero(BaseHero hero)
    {
        SelectedHero = hero;
        MenuManager.Instance.ShowSelectedHero(hero);
    }

    public void MovePlayer()
    {
        // FIXME: Fire 改為當前玩家角色名稱
        var fireTile = GridManager.Instance.GetTileByName("Fire");
        var dicePoint = DiceManager.Instance.dicePoint;
        var position = GetMovePosition(fireTile, dicePoint);
        var hero = (BaseHero)fireTile.OccupiedUnit;

        var movedTile = GridManager.Instance.GetTileAtPosition(position);
        movedTile.SetUnit(hero);
    }

    // 移動邏輯
    private Vector2 GetMovePosition(Tile tile, int dicePoint)
    {
        // 當前位置
        var currentPositionX = tile.transform.position.x;
        var currentPositionY = tile.transform.position.y;

        // 要移動的位置
        var movePositionX = currentPositionX;
        var movePositionY = currentPositionY;

        if (currentPositionX == 0)
        {
            movePositionY = currentPositionY + dicePoint;
            // 該轉彎了
            if (movePositionY > 7)
            {
                var remainPoint = 7 - currentPositionY;
                movePositionY = 7;
                movePositionX = remainPoint;
            }
        }

        return new Vector2(movePositionX, movePositionY);
    } 
}
