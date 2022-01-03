using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager1 : MonoBehaviour
{
    public static UnitManager1 Instance;
    public BaseHero SelectedHero;
    List<ScriptableUnit> units;

    private void Awake()
    {
        Instance = this;

        units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnHeros()
    {
        var heros = new string[] { "Fire" };

        foreach (var hero in heros)
        {
            //var firePrefab = GetUnitByName<BaseHero>(Faction.Heroes, hero);
            //var spawnedHero = Instantiate(firePrefab);
            //var randomSpawnTile = GridManager1.Instance.GetHeroSpawnTile();

            //randomSpawnTile.SetUnit(spawnedHero);
        }

        DiceManager.Instance.ShowDiceButton();
        GameManager1.Instance.ChangeState((GameState)GameState1.HerosTurn);
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
        return (T)units.Where(u => u.UnitPrefab.UnitName == name).First().UnitPrefab;
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)units.Where(u => u.name == "").OrderBy(o => Random.value).First().UnitPrefab;
    }

    public void SetSelectedHero(BaseHero hero)
    {
        SelectedHero = hero;
        MenuManager.Instance.ShowSelectedHero(hero);
    }

    //public IEnumerator MovePlayer()
    //{
    //    // FIXME: Fire 改為當前玩家角色名稱
    //    //var fireTile = GridManager1.Instance.GetTileByName("Fire");
    //    var dicePoint = DiceManager.Instance.dicePoint;
    //    //var positions = GetMovePosition(fireTile, dicePoint);
    //    //var hero = (BaseHero)fireTile.OccupiedUnit;

    //    //foreach (var position in positions)
    //    //{
    //    //    var movedTile = GridManager1.Instance.GetTileAtPosition(position);
    //    //    yield return new WaitForSeconds(1);
    //    //    //movedTile.SetUnit(hero);
    //    //}
    //}

    // 移動邏輯 回傳 Vector 陣列為了讓他有一格一格走的效果 TODO: 考慮之後會換地圖 x y 不會是固定的
    private Vector2[] GetMovePosition(Tile tile, int dicePoint)
    {
        var positions = new List<Vector2>();

        // 當前位置
        var currentPositionX = tile.transform.position.x;
        var currentPositionY = tile.transform.position.y;

        if (currentPositionX == 0)
        {
            var totalMoveCount = currentPositionY + dicePoint;
            if (totalMoveCount > 7)
            {
                for (int i = 0; i < 7 - currentPositionY; i++)
                {
                    positions.Add(new Vector2(x: 0, y: currentPositionY + i + 1));
                }
                var remainPoint = Mathf.Abs(7 - totalMoveCount);
                for (int i = 0; i < remainPoint; i++)
                {
                    positions.Add(new Vector2(x: i + 1, y: 7));
                }
            }
            else
            {
                for (int i = 0; i < dicePoint; i++)
                {
                    positions.Add(new Vector2(x: 0, y: currentPositionY + i + 1));
                }
            }
        }
        else if (currentPositionY == 7)
        {
            var totalMoveCount = currentPositionX + dicePoint;
            if (totalMoveCount > 9)
            {
                for (int i = 0; i < 9 - currentPositionX; i++)
                {
                    positions.Add(new Vector2(x: currentPositionX + i + 1, y: 7));
                }
                var remainPoint = Mathf.Abs(9 - totalMoveCount);
                for (int i = 0; i < remainPoint; i++)
                {
                    // 多扣一
                    positions.Add(new Vector2(x: 9, y: 7 - i - 1));
                }
            }
            else
            {
                for (int i = 0; i < dicePoint; i++)
                {
                    positions.Add(new Vector2(x: currentPositionX + i + 1, y: 7));
                }
            }
        }
        else if (currentPositionX == 9)
        {
            var movePositionY = currentPositionY - dicePoint;
            if (movePositionY < 0)
            {
                for (int i = 0; i < currentPositionY; i++)
                {
                    positions.Add(new Vector2(x: 9, y: currentPositionY - i - 1));
                }
                var remainPoint = Mathf.Abs(currentPositionY - dicePoint);
                for (int i = 0; i < remainPoint; i++)
                {
                    positions.Add(new Vector2(x: 9 - i - 1, y: 0));
                }
            }
            else
            {
                for (int i = 0; i < dicePoint; i++)
                {
                    positions.Add(new Vector2(x: 9, y: currentPositionY - i - 1));
                }
            }
        }
        else if (currentPositionY == 0)
        {
            var movePositionX = currentPositionX - dicePoint;
            if (movePositionX < 0)
            {
                for (int i = 0; i < currentPositionX; i++)
                {
                    positions.Add(new Vector2(x: currentPositionX - i -1, y: 0));
                }
                var remainPoint = Mathf.Abs(currentPositionX - dicePoint);
                for (int i = 0; i < remainPoint; i++)
                {
                    positions.Add(new Vector2(x: 0, y: i + 1));
                }
            }
            else
            {
                for (int i = 0; i < dicePoint; i++)
                {
                    positions.Add(new Vector2(x: currentPositionX - i - 1, y: 0));
                }
            }
        }

        return positions.ToArray();
    }
}
