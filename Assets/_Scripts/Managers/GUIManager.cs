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
