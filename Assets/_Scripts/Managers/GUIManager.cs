using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �޲z GUI �ƥ�
/// </summary>
public class GUIManager : StaticInstance<GUIManager>
{
    [SerializeField] GameObject EndRoundButton;

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
