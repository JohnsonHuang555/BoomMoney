using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理所有地圖效果，如火力/毒氣範圍，剩餘回合歸零後Destroy
/// </summary>
public class EffectManager : StaticInstance<EffectManager>
{
    public void SpawnEffect()
    {

    }
}

public enum Effect
{
    Fire = 0, // 火力
    Poison = 1 // 毒氣
}
