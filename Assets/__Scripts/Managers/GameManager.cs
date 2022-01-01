using System;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 Instance;
    public GameState GameState;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState((GameState)__GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case (GameState)__GameState.GenerateGrid:
                GridManager1.Instance.GenerateGrid();
                break;
            case (GameState)__GameState.SpawnHeros:
                UnitManager.Instance.SpawnHeros();
                break;
            case (GameState)__GameState.SpawnItems:
                // TODO: ¹D¨ã±¼¸¨
                break;
            case (GameState)__GameState.HerosTurn:
                break;
            case (GameState)__GameState.MovePlayer:
                //StartCoroutine(UnitManager.Instance.MovePlayer());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum __GameState
{
    GenerateGrid = 0,
    SpawnHeros = 1,
    SpawnItems = 2,
    HerosTurn = 3,
    MovePlayer = 4,
}
