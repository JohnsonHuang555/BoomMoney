using UnityEngine;
using System;
using Lean.Gui;

/// <summary>
/// �޲z�C���D�n�y�{�A�}�l�^�X�A���a�^�X�A�����^�X����
/// </summary>
public class GameManager : StaticInstance<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged; 
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

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
                // TODO: ���ᰵ
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
    /// �a�J�e�ӳ���������򶶧ǡA���@�Ǫ�l�ƨƥ�A�M�w���ǡA��l�˳Ƥ�����
    /// </summary>
    private void HandleStarting()
    {
        ChangeState(GameState.GenerateMap);
    }

    /// <summary>
    /// �ͦ��a��
    /// </summary>
    private void HandleGenerateMap()
    {
        // FIXME: TestData
        MapManager.Instance.GenerateMap(TestData.Overworld);
        ChangeState(GameState.SpawningPlayers);
    }

    /// <summary>
    /// �H���ͦ����a
    /// </summary>
    private void HandleSpawnPlayers()
    {
        UnitManager.Instance.SpawnPlayers();
        ChangeState(GameState.PlayerTurn);
    }

    /// <summary>
    /// �H���ͦ��D��
    /// </summary>
    private void HandleSpawnItems()
    {
    }

    /// <summary>
    /// ����U�@�쪱�a
    /// </summary>
    private void HandlePlayerTurn()
    {
        // TODO: �M�w���Ӫ��a��� GUI
        LeanToggle.TurnOnAll("SetBombModal");
        GUIManager.Instance.ShowEndRoundButton();
    }

    /// <summary>
    /// �B�z�����޿�
    /// </summary>
    private void HandleMovePlayer()
    {
        StartCoroutine(UnitManager.Instance.MovePlayer());
    }

    /// <summary>
    /// �p���z���ĪG�ζˮ`
    /// </summary>
    private void HandleBombExplode()
    {
        // ����z���ĪG
        EffectManager.Instance.SpawnEffect(Effect.Explosion);
        // TODO: �p��ˮ`�A�ˬd�O�_���a��q�k�s �O�Y��ӡA�Ϥ����U�@�쪱�a
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
