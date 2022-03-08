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

    [SerializeField] GameObject CardArea;
    [SerializeField] public GameObject DropZone;

    List<CardBase> cards = new();

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
        // 取出存在 resource 的卡片資料
        var scriptableCards = ResourceSystem.Instance.Cards;

        foreach (var card in scriptableCards)
        {
            cards.Add(card.Prefab);
        }
    }

    private void SpawnCards()
    {
        for (int i = 0; i < 5; i++)
        {
            var randomCard = cards[Random.Range(0, cards.Count)];
            var card = Instantiate(randomCard.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
            card.transform.SetParent(CardArea.transform, false);
        }
    }
}
