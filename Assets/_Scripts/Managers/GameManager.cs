using UnityEngine;
using System;
using Lean.Gui;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// �޲z�C���D�n�y�{�A�}�l�^�X�A���a�^�X�A�����^�X����
/// </summary>
public class GameManager : StaticInstance<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    // ��e���a�A�]�����⤣�|����
    [SerializeField] public CharacterName CurrentPlayer;

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
        // ���o���쬰�@�����a
        CurrentPlayer = TestData.FirstPlayer;
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

        // �p�⬵�u�ˮ`
        DemageCalculateHelper.CalculateBombDemage();

        // �g�J�U�@�쪱�a
        CurrentPlayer = PlayerHelper.GetNewCurrentPlayer(CurrentPlayer);

        // TODO: �p��ˮ`�A�ˬd�O�_���a��q�k�s �O�Y��ӡA�Ϥ����U�@�쪱�a
        var gameOver = CheckGameOver();
        if (gameOver.isGameOver)
        {
            // �����C��
        }
        else
        {
            ChangeState(GameState.PlayerTurn);
        }
    }

    /// <summary>
    /// �ˬd�C���O�_����
    /// </summary>
    /// <returns></returns>
    private GameOver CheckGameOver()
    {
        var characterGameObjects = UnitManager.Instance.GetCharacterGameObject();

        // TODO: �p�G�O�ζ��Ҧ����ܭn���t�~�P�_
        // �̫�s�����a
        var remainedPlayer = characterGameObjects.Where(c => {
            var unit = c.GetComponent<CharacterUnitBase>();
            return unit.Stats.Health > 0;
        }).Select(c => c.GetComponent<CharacterUnitBase>()).ToList();

        Debug.Log(remainedPlayer.Count);
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
    // TODO: �b���ժ��ɭԷ|����
    public int winnerGroup;
}
