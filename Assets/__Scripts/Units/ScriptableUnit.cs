using UnityEngine;

[CreateAssetMenu(fileName ="New Unit", menuName ="Scriptable unit")]
public class ScriptableUnit : ScriptableObject
{
    public __Faction Faction;
    public BaseUnit UnitPrefab;
}

public enum __Faction
{
    Hero = 0,
    Enemy = 1,
}
