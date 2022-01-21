using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUnitBase : UnitBase
{
    public virtual void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

    public virtual void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;
    private void OnStateChanged(GameState newState)
    {
        // ���@�Ǧ@�P���Ʊ�
    }
}
