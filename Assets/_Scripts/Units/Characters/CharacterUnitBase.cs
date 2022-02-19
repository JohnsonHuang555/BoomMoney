using UnityEngine;

public class CharacterUnitBase : UnitBase
{
    public CharacterStats Stats { get; private set; }
    public virtual void SetStats(CharacterStats stats) => Stats = stats;
    private void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;
    
    private void OnStateChanged(GameState newState)
    {

    }

    public virtual void TakeDamage(int dmg)
    {
        
    }

    public virtual void ExecuteMove()
    {
        // Override this to do some hero-specific logic, then call this base method to clean up the turn
    }
}
