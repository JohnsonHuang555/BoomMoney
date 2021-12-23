using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnHeros:
                UnitManager.Instance.SpawnHeros();
                break;
            case GameState.SpawnItems:
                // TODO: ¹D¨ã±¼¸¨
                break;
            case GameState.HerosTurn:
                break;
            case GameState.MovePlayer:
                Debug.Log("MOVE");
                UnitManager.Instance.MovePlayer();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnHeros = 1,
    SpawnItems = 2,
    HerosTurn = 3,
    MovePlayer = 4,
}
