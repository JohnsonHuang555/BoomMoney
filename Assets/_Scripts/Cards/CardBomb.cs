using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBomb : CardBase
{
    public override void ExecuteCard()
    {
        UnitManager.Instance.SpawnBomb();
    }
}
