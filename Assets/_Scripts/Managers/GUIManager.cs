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
    [SerializeField] public GameObject DropZone;

    List<GameObject> cards = new();

    private void Start()
    {
        // 設定所有卡片
        SetCards();

        // 把卡片畫在畫面上
        SpawnCards();
    }

    public void ShowDropZone(bool show)
    {
        DropZone.SetActive(show);
    }

    public void ShowEndRoundButton()
    {
        EndRoundButton.SetActive(true);
    }

    // 結束回合即移動玩家
    public void OnEndRound()
    {
        if (GameManager.Instance.State != GameState.PlayerTurn)
        {
            return;
        }

        EndRoundButton.SetActive(false);

        // 擲骰子移動
        DiceManager.Instance.RollDice();
    }

    private void SetCards()
    {
        cards.Add(Card1);
        cards.Add(Card2);
    }

    private void SpawnCards()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject card1 = Instantiate(cards[Random.Range(0, cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
            card1.transform.SetParent(CardArea.transform, false);
        }
    }
}
