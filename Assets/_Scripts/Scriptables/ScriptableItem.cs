using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Create a scriptable item 
/// </summary>
[CreateAssetMenu(fileName = "New Scriptable Example")]
public class ScriptableItem : ScriptableUnitBase
{
    public ItemType ItemType;

    [SerializeField] private ItemStats _stats;

    public ItemStats BaseStats => _stats;
    // Used in game
    public ItemUnitBase Prefab;
}

[Serializable]
public enum ItemType
{
    Bomb = 0,
}

[Serializable]
public struct ItemStats
{
    public int RemainedRound;
    public int Fire;
    public Vector2 Position;
}
