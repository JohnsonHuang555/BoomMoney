using System;
using UnityEngine;

/// <summary>
/// Àx¦s¸ê®Æ
/// </summary>
public class ScriptableUnitBase : ScriptableObject
{
    public Faction Faction;
}

[Serializable]
public enum Faction
{
    Characters,
    Card,
}
