using UnityEngine;

/// <summary>
/// �޲z��l�޿�A�I�ƥi��|�]���d���ĪG���ܰ�
/// </summary>
public class DiceManager : StaticInstance<DiceManager>
{
    public int dicePoint = 0;

    public void RollDice()
    {
        var point = Random.Range(1, 7);
        dicePoint = 1;

        GameManager.Instance.ChangeState(GameState.MovePlayer);
    }
}
