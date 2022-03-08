using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// �޲z GUI �ƥ�
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
        // �]�w�Ҧ��d��
        SetCards();

        // ��d���e�b�e���W
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

    // �����^�X�Y���ʪ��a
    public void OnEndRound()
    {
        if (GameManager.Instance.State != GameState.PlayerTurn)
        {
            return;
        }

        EndRoundButton.SetActive(false);

        // �Y��l����
        DiceManager.Instance.RollDice();
    }

    private void SetCards()
    {
        // ���X�s�b resource ���d�����
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
