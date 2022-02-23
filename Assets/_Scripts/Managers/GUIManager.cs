using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理 GUI 事件
/// </summary>
public class GUIManager : StaticInstance<GUIManager>
{
    [SerializeField] GameObject EndRoundButton;

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
