using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static event Action<GameState> OnBeforeStateChanged; 
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    // Start is called before the first frame update
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
                break;
            case GameState.SpawningItems:
                break;
            case GameState.PlayerTurn:
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
        ChangeState(GameState.GenerateMap);
    }

    private void HandleGenerateMap()
    {
        MapManager.Instance.GenerateMap();
        ChangeState(GameState.SpawningPlayers);
    }
}

[Serializable]
public enum GameState
{
    Starting = 0,
    GenerateMap = 1,
    SpawningPlayers = 2,
    SpawningItems = 3,
    PlayerTurn = 4,
    Win = 5,
    Lose = 6,
}
