using System;
using UnityEngine;

public class ScriptableUnitBase : ScriptableObject
{
    public Faction Faction;

    [SerializeField] private Stats _stats;

    public Stats BaseStats => _stats;

    // Used in game
    public CharacterUnitBase Prefab;

    // Used in menus
    public Sprite MenuSprite;
}

[Serializable]
public struct Stats
{
    public int Health;
}

[Serializable]
public enum Faction
{
    Characters = 0,
}
