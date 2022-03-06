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

    // Cards
    [SerializeField] GameObject Card1;
    [SerializeField] GameObject Card2;
    [SerializeField] GameObject CardArea;

    List<GameObject> cards = new();

    private void Start()
    {
        cards.Add(Card1);
        cards.Add(Card2);

        for (int i = 0; i < 5; i++)
        {
            GameObject card1 = Instantiate(cards[Random.Range(0, cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
            card1.transform.SetParent(CardArea.transform, false);
        }
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
