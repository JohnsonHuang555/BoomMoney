using System;
using UnityEngine;

public class ScriptableUnitBase : ScriptableObject
{
    public Faction Faction;

    [SerializeField] private Stats _stats;

    public Stats BaseStats => _stats;
}

[Serializable]
public enum Faction
{
    Characters = 0,
    Items = 1,
}
