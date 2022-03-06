using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 管理 GUI 事件
/// </summary>
public class GUIManager : StaticInstance<GUIManager>
{
    [SerializeField] GameObject EndRoundButton;
    [SerializeField] TextMeshProUGUI HealthValue;

    [SerializeField] GameObject Card1;
    [SerializeField] GameObject CardArea;

    private void Start()
    {
        GameObject card1 = Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity);
        card1.transform.SetParent(CardArea.transform, false);
    }

    public void ShowEndRoundButton()
    {
        EndRoundButton.SetActive(true);
    }

    // 結束回合即移動玩家
    public void OnEndRound()
    {
        // TODO: 寫一個 isCurrentPlayer 變數存是不是輪到該玩家否則 retrun，非玩家回合不得擲骰子
        if (GameManager.Instance.State != GameState.PlayerTurn)
        {
            return;
        }

        EndRoundButton.SetActive(false);

        // 擲骰子移動
        DiceManager.Instance.RollDice();
    }
}
