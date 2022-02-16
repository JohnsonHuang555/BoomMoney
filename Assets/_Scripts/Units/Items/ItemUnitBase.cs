using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �b�a�ϤW�|�ͦ����D��άO�ĪG
/// </summary>
public class ItemUnitBase : UnitBase
{
    public ItemStats Stats { get; private set; }
    public virtual void SetStats(ItemStats stats) => Stats = stats;

    public virtual void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

    public virtual void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;
    private void OnStateChanged(GameState newState)
    {
        // ���@�Ǧ@�P���Ʊ�
    }
}
