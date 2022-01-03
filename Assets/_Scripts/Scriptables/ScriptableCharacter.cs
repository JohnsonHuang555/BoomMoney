using System;
using UnityEngine;

/// <summary>
/// Create a scriptable player 
/// </summary>
[CreateAssetMenu(fileName = "New Scriptable Example")]
public class ScriptableCharacter : ScriptableUnitBase
{
    public CharacterType CharacterType;
}

[Serializable]
public enum CharacterType
{
    Fire = 0,
}
