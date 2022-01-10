using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUnitBase : UnitBase
{
    private void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;
    private void OnStateChanged(GameState newState)
    {

    }
}
