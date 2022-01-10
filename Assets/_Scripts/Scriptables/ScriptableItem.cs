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

    // Used in game
    public ItemUnitBase Prefab;
}

[Serializable]
public enum ItemType
{
    Bomb = 0,
}
