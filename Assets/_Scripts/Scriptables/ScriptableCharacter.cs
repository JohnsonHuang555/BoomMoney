using System;
using UnityEngine;

/// <summary>
/// Create a scriptable character 
/// </summary>
[CreateAssetMenu(fileName = "New Scriptable Example")]
public class ScriptableCharacter : ScriptableUnitBase
{
    public CharacterName CharacterName;

    [SerializeField] private CharacterStats _stats;

    public CharacterStats BaseStats => _stats;

    // Used in game
    public CharacterUnitBase Prefab;
}

public struct Player
{
    public int Id;
    public int PlayOrder;
}

// 角色名稱
[Serializable]
public enum CharacterName
{
    Fire, // 暫定以後加新的把這刪掉
    Green
}

// 角色狀態
[Serializable]
public struct CharacterStats
{
    public int Health; // 血量
}

