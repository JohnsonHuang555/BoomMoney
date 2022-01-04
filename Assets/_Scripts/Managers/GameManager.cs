using UnityEngine;
using System;

public class GameManager : StaticInstance<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged; 
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    // Kick the game off with the first state
    void Start() => ChangeState(GameState.Starting);

    public void ChangeState(GameState newState)
    {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;

        switch (newState)
        {
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.GenerateMap:
                HandleGenerateMap();
                break;
            case GameState.SpawningPlayers:
                HandleSpawnPlayers();
                break;
            case GameState.SpawningItems:
                // TODO: ¤§«á°µ
                break;
            case GameState.PlayerTurn:
                HandlePlayerTurn();
                break;
            case GameState.MovePlayer:
                HandleMovePlayer();
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null); ;
        }

        OnAfterStateChanged?.Invoke(newState);

        Debug.Log($"New state: {newState}");
    }

    private void HandleStarting()
    {
        // Do some start setup, could be environment, cinematics etc

        // Eventually call ChangeState again with your next state
        ChangeState(GameState.GenerateMap);
    }

    private void HandleGenerateMap()
    {
        MapManager.Instance.GenerateMap();
        ChangeState(GameState.SpawningPlayers);
    }

    private void HandleSpawnPlayers()
    {
        UnitManager.Instance.SpawnPlayers();
        ChangeState(GameState.PlayerTurn);
    }

    private void HandlePlayerTurn()
    {
        // If you're making a turn based game, this could show the turn menu, highlight available units etc

        // Keep track of how many units need to make a move, once they've all finished, change the state. This could
        // be monitored in the unit manager or the units themselves.
        DiceManager.Instance.ShowDiceButton();
    }

    private void HandleMovePlayer()
    {
        DiceManager.Instance.ShowDiceButton();
        StartCoroutine(UnitManager.Instance.MovePlayer());
    }
}

/// <summary>
/// This is obviously an example and I have no idea what kind of game you're making.
/// You can use a similar manager for controlling your menu states or dynamic-cinematics, etc
/// </summary>
[Serializable]
public enum GameState
{
    Starting = 0,
    GenerateMap = 1,
    SpawningPlayers = 2,
    SpawningItems = 3,
    PlayerTurn = 4,
    MovePlayer = 5,
    Win = 6,
    Lose = 7,
}
