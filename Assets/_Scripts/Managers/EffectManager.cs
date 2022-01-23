using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理所有地圖效果，如火力/毒氣範圍，剩餘回合歸零後Destroy
/// </summary>
public class EffectManager : StaticInstance<EffectManager>
{
    public void SpawnEffect(Effect effect)
    {
        switch (effect)
        {
            case Effect.Fire:
                if (Helpers.DoesTagExist("Bomb"))
                {
                    var bombsOnMap = GameObject.FindGameObjectsWithTag("Bomb");

                    foreach (var bomb in bombsOnMap)
                    {
                        Debug.Log(bomb.GetComponent<Bomb>().fire);
                    }
                }
                break;
            case Effect.Poison:
                break;
            default:
                throw new ArgumentOutOfRangeException("Effect not found");
        }
    }
}

public enum Effect
{
    Fire = 0, // 火力
    Poison = 1 // 毒氣
}
