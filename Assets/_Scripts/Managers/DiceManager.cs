using UnityEngine;

/// <summary>
/// 管理骰子邏輯，點數可能會因為卡片效果而變動
/// </summary>
public class DiceManager : StaticInstance<DiceManager>
{
    public int dicePoint = 0;
    [SerializeField] GameObject diceButton;

    public void ShowDiceButton()
    {
        diceButton.SetActive(true);
    }

    public void RollDice()
    {
        if (GameManager.Instance.State != GameState.PlayerTurn)
        {
            return;
        }

        diceButton.SetActive(false);

        var point = Random.Range(1, 7);
        dicePoint = point;

        GameManager.Instance.ChangeState(GameState.MovePlayer);
    }
}
