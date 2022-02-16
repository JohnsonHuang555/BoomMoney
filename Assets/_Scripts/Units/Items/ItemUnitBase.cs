using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 在地圖上會生成的道具或是效果
/// </summary>
public class ItemUnitBase : UnitBase
{
    public ItemStats Stats { get; private set; }
    public virtual void SetStats(ItemStats stats) => Stats = stats;

    public virtual void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

    public virtual void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;
    private void OnStateChanged(GameState newState)
    {
        // 做一些共同的事情
    }
}
