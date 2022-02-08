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

// 角色名稱
[Serializable]
public enum CharacterName
{
    Fire = 0,
}

// 角色狀態
[Serializable]
public struct CharacterStats
{
    public int Health; // 血量
    public int Fire; // 火力
    public int Bombs; // 持有炸彈數
    public ItemType[] Items; // 持有道具
}

