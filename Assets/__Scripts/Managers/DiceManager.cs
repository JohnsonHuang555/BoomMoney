using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance;
    public int dicePoint = 0;
    [SerializeField] GameObject diceButton;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowDiceButton()
    {
        diceButton.SetActive(true);
    }

    public void RollDice()
    {
        //if (GameManager.Instance.GameState != GameState.HerosTurn)
        //{
        //    return;
        //}

        diceButton.SetActive(false);

        var point = Random.Range(1, 7);
        dicePoint = point;
        //GameManager.Instance.ChangeState(GameState.MovePlayer);
    }
}
