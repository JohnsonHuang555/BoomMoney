using UnityEngine;

public class CharacterUnitBase : UnitBase
{
    private void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;
    
    private void OnStateChanged(GameState newState)
    {

    }

    public virtual void ExecuteMove()
    {
        // Override this to do some hero-specific logic, then call this base method to clean up the turn
    }
}
