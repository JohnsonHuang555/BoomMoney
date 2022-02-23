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

// ����W��
[Serializable]
public enum CharacterName
{
    Fire, // �ȩw�H��[�s����o�R��
    Green
}

// ���⪬�A
[Serializable]
public struct CharacterStats
{
    public int Health; // ��q
}

