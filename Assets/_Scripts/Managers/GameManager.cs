using UnityEngine;
using System;
using Lean.Gui;

/// <summary>
/// 管理遊戲主要流程，開始回合，玩家回合，結束回合等等
/// </summary>
public class GameManager : StaticInstance<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged; 
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    // FIXME: 暫時放這
    [SerializeField] GameObject EndRoundButton;

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
                // TODO: 之後做
                break;
            case GameState.PlayerTurn:
                HandlePlayerTurn();
                break;
            case GameState.MovePlayer:
                HandleMovePlayer();
                break;
            case GameState.BombExplode:
                HandleBombExplode();
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);

        Debug.Log($"New state: {newState}");
    }

    public void ShowEndRoundButton()
    {
        EndRoundButton.SetActive(true);
    }

    // 結束回合即移動玩家
    public void OnEndRound()
    {
        // TODO: 寫一個 isCurrentPlayer 變數存是不是輪到該玩家否則 retrun，非玩家回合不得擲骰子
        if (State != GameState.PlayerTurn)
        {
            return;
        }

        EndRoundButton.SetActive(false);

        // 擲骰子移動
        DiceManager.Instance.RollDice();
        //ChangeState(GameState.BombExplode);
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

    private void HandleSpawnPlayers()
    {
        UnitManager.Instance.SpawnPlayers();
        ChangeState(GameState.PlayerTurn);
    }

    private void HandlePlayerTurn()
    {
        // TODO: 決定哪個玩家顯示 GUI
        LeanToggle.TurnOnAll("SetBombModal");
        ShowEndRoundButton();
    }

    private void HandleMovePlayer()
    {
        StartCoroutine(UnitManager.Instance.MovePlayer());
    }

    private void HandleBombExplode()
    {
        // 顯示爆炸效果
        EffectManager.Instance.SpawnEffect(Effect.Fire);
        // TODO: 計算傷害，檢查是否玩家血量歸零 是即獲勝，反之換下一位玩家
        ChangeState(GameState.PlayerTurn);
    }
}

/// <summary>
/// This is obviously an example and I have no idea what kind of game you're making.
/// You can use a similar manager for controlling your menu states or dynamic-cinematics, etc
/// </summary>
[Serializable]
public enum GameState
{
    Starting,
    GenerateMap,
    SpawningPlayers,
    SpawningItems,
    PlayerTurn,
    MovePlayer,
    BombExplode,
    Win,
    Lose,
}
