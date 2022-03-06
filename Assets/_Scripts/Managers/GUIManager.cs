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

    // �����^�X�Y���ʪ��a
    public void OnEndRound()
    {
        // TODO: �g�@�� isCurrentPlayer �ܼƦs�O���O����Ӫ��a�_�h retrun�A�D���a�^�X���o�Y��l
        if (GameManager.Instance.State != GameState.PlayerTurn)
        {
            return;
        }

        EndRoundButton.SetActive(false);

        // �Y��l����
        DiceManager.Instance.RollDice();
    }
}
