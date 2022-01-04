using UnityEngine;

public class CharacterUnitBase : UnitBase
{
    private bool canMove;
    private void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;
    private void OnStateChanged(GameState newState)
    {
        if (newState == GameState.PlayerTurn) canMove = true;
    }

    public virtual void ExecuteMove()
    {
        // Override this to do some hero-specific logic, then call this base method to clean up the turn

        canMove = false;
    }
}
