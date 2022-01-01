using System;
using UnityEngine;

/// <summary>
/// Nice, easy to understand enum-based game manager. For larger and more complex games, look into
/// state machines. But this will serve just fine for most games.
/// </summary>
public class ExampleGameManager : StaticInstance<ExampleGameManager> {
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    // Kick the game off with the first state
    void Start() => ChangeState(GameState.Starting);

    public void ChangeState(GameState newState) {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState) {
            //case GameState1.Starting:
            //    HandleStarting();
            //    break;
            //case GameState1.SpawningHeroes:
            //    HandleSpawningHeroes();
            //    break;
            //case GameState1.SpawningEnemies:
            //    HandleSpawningEnemies();
            //    break;
            //case GameState1.HeroTurn:
            //    HandleHeroTurn();
            //    break;
            //case GameState1.EnemyTurn:
            //    break;
            //case GameState.Win:
            //    break;
            //case GameState.Lose:
            //    break;
            //default:
            //    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
        
        Debug.Log($"New state: {newState}");
    }

    private void HandleStarting() {
        // Do some start setup, could be environment, cinematics etc

        // Eventually call ChangeState again with your next state
        
        ChangeState((GameState)GameState1.SpawningHeroes);
    }

    private void HandleSpawningHeroes() {
        ExampleUnitManager.Instance.SpawnHeroes();
        
        ChangeState((GameState)GameState1.SpawningEnemies);
    }

    private void HandleSpawningEnemies() {
        
        // Spawn enemies
        
        ChangeState((GameState)GameState1.HeroTurn);
    }

    private void HandleHeroTurn() {
        // If you're making a turn based game, this could show the turn menu, highlight available units etc
        
        // Keep track of how many units need to make a move, once they've all finished, change the state. This could
        // be monitored in the unit manager or the units themselves.
    }
}

/// <summary>
/// This is obviously an example and I have no idea what kind of game you're making.
/// You can use a similar manager for controlling your menu states or dynamic-cinematics, etc
/// </summary>
[Serializable]
public enum GameState1 {
    Starting = 0,
    SpawningHeroes = 1,
    SpawningEnemies = 2,
    HeroTurn = 3,
    EnemyTurn = 4,
    Win = 5,
    Lose = 6,

    //
    HerosTurn = 7,
}