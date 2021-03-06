using UnityEngine;
using System;
using Lean.Gui;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// 管理遊戲主要流程，開始回合，玩家回合，結束回合等等
/// </summary>
public class GameManager : StaticInstance<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    // 當前玩家，因為角色不會重複
    [SerializeField] public CharacterName CurrentPlayer { get; private set; }
    [SerializeField] public bool isNewRound { get; private set; }

    public void SetNewCurrentPlayer(CharacterName newCurrentPlayer)
    {
        var players = TestData.GetPlayers();
        // 回到第一位玩家代表過了一回合
        if (players[newCurrentPlayer].PlayOrder == 0)
        {
            isNewRound = true;
        }
        else
        {
            isNewRound = false;
        }
        CurrentPlayer = newCurrentPlayer;
    }

    // Kick the game off with the first state
    void Start() => ChangeState(GameState.Starting);

    public void ChangeState(GameState newState)
    {
        //if (newState == State) { return; }
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

    /// <summary>
    /// 帶入前個場景的角色跟順序，做一些初始化事件，決定順序，初始裝備之類的
    /// </summary>
    private void HandleStarting()
    {
        // 取得順位為一的玩家
        CurrentPlayer = TestData.FirstPlayer;
        ChangeState(GameState.GenerateMap);
    }

    /// <summary>
    /// 生成地圖
    /// </summary>
    private void HandleGenerateMap()
    {
        // FIXME: TestData
        MapManager.Instance.GenerateMap(TestData.Overworld);
        ChangeState(GameState.SpawningPlayers);
    }

    /// <summary>
    /// 隨機生成玩家
    /// </summary>
    private void HandleSpawnPlayers()
    {
        UnitManager.Instance.SpawnPlayers();
        ChangeState(GameState.PlayerTurn);
    }

    /// <summary>
    /// 隨機生成道具
    /// </summary>
    private void HandleSpawnItems()
    {
    }

    /// <summary>
    /// 輪到下一位玩家
    /// </summary>
    private void HandlePlayerTurn()
    {
        // TODO: 決定哪個玩家顯示 GUI
        //LeanToggle.TurnOnAll("SetBombModal");
        GUIManager.Instance.ShowEndRoundButton();
    }

    /// <summary>
    /// 處理移動邏輯
    /// </summary>
    private void HandleMovePlayer()
    {
        StartCoroutine(UnitManager.Instance.MovePlayer());
    }

    /// <summary>
    /// 計算爆炸效果及傷害
    /// </summary>
    private void HandleBombExplode()
    {
        // 顯示爆炸效果
        EffectManager.Instance.SpawnEffect(Effect.Explosion);

        // 計算炸彈傷害
        DemageCalculateHelper.CalculateBombDemage();

        // TODO: 計算傷害，檢查是否玩家血量歸零 是即獲勝，反之換下一位玩家
        var gameOver = CheckGameOver();
        if (gameOver.isGameOver)
        {
            // 結束遊戲
        }
        else
        {
            ChangeState(GameState.PlayerTurn);
        }
    }

    /// <summary>
    /// 檢查遊戲是否結束
    /// </summary>
    /// <returns></returns>
    private GameOver CheckGameOver()
    {
        var characterGameObjects = UnitManager.Instance.GetGameObjects("Character");

        // TODO: 如果是團隊模式的話要做另外判斷
        // 最後存活玩家
        var remainedPlayer = characterGameObjects.Where(c =>
        {
            var unit = c.GetComponent<CharacterUnitBase>();
            return unit.Stats.Health > 0;
        }).Select(c => c.GetComponent<CharacterUnitBase>()).ToList();

        if (remainedPlayer.Count == 1)
        {
            var winner = (CharacterName)remainedPlayer[0].UnitName;
            return new GameOver { isGameOver = true, winner = winner };
        }
        return new GameOver { isGameOver = false };
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

public partial struct GameOver
{
    public bool isGameOver;
    public CharacterName winner;
    // TODO: 在分組的時候會有值
    public int winnerGroup;
}
