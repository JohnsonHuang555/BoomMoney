using System;
using UnityEngine;

/// <summary>
/// �x�s���
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
