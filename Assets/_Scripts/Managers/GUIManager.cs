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
    [SerializeField] public GameObject DropZone;

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
}
