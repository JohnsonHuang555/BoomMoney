using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �޲z�Ҧ����a�d��
/// </summary>
public class CardManager : StaticInstance<CardManager>
{
    private Dictionary<CharacterName, CardBase> playerHoldCards;
    List<CardBase> allCards = new();
    [SerializeField] GameObject CardArea;

    private void Start()
    {
        // �]�w�Ҧ��d��
        SetCards();

        // ��d���e�b�e���W
        SpawnCards();

        var players = TestData.GetPlayers();
        foreach (var player in players)
        {

        }
    }

    private void SetCards()
    {
        // ���X�s�b resource ���d�����
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
