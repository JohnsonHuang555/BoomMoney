using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Create a scriptable card 
/// </summary>
[CreateAssetMenu(fileName = "New Scriptable Example")]
public class ScriptableCard : ScriptableUnitBase
{
    public CardType CardType;

    public CardBase Prefab;
}

[Serializable]
public enum CardType
{
    Item, // �D��d
    Skill, // �ޯ�d
    Event, // �ƥ�d
}