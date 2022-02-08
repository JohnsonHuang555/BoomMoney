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

// ����W��
[Serializable]
public enum CharacterName
{
    Fire = 0,
}

// ���⪬�A
[Serializable]
public struct CharacterStats
{
    public int Health; // ��q
    public int Fire; // ���O
    public int Bombs; // �������u��
    public ItemType[] Items; // �����D��
}

