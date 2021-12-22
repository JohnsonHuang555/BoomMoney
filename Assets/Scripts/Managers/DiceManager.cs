using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public static DiceManager instance;
    public int dicePoint = 0;

    private void Awake()
    {
        instance = this;
    }

    public void RollDice()
    {
        var point = Random.Range(1, 7);
        dicePoint = point;
    }
}
