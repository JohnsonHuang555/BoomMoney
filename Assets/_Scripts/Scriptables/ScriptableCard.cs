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
    Item, // 道具卡
    Skill, // 技能卡
    Event, // 事件卡
}