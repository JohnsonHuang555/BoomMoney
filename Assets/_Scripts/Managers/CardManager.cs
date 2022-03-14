using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理所有玩家卡片
/// </summary>
public class CardManager : StaticInstance<CardManager>
{
    private Dictionary<CharacterName, CardBase> playerHoldCards;
    List<CardBase> allCards = new();
    [SerializeField] GameObject CardArea;

    private void Start()
    {
        // 設定所有卡片
        SetCards();

        // 把卡片畫在畫面上
        SpawnCards();

        var players = TestData.GetPlayers();
        foreach (var player in players)
        {

        }
    }

    private void SetCards()
    {
        // 取出存在 resource 的卡片資料
        var scriptableCards = ResourceSystem.Instance.Cards;

        foreach (var card in scriptableCards)
        {
            allCards.Add(card.Prefab);
        }
    }

    private void SpawnCards()
    {
        for (int i = 0; i < 5; i++)
        {
            var randomCard = allCards[Random.Range(0, allCards.Count)];
            var card = Instantiate(randomCard.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
            card.transform.SetParent(CardArea.transform, false);
        }
    }
}
