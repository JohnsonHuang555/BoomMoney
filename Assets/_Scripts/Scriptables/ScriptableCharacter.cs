using System;
using UnityEngine;

/// <summary>
/// Create a scriptable character 
/// </summary>
[CreateAssetMenu(fileName = "New Scriptable Example")]
public class ScriptableCharacter : ScriptableUnitBase
{
    public CharacterType CharacterType;

    // Used in game
    public CharacterUnitBase Prefab;
}

[Serializable]
public enum CharacterType
{
    Fire = 0,
}

[Serializable]
public struct Stats
{
    public int Health;
}

