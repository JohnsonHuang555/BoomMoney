using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBomb : CardBase
{
    public override IEnumerator UseCard()
    {
        yield return new WaitForSeconds(2);

        GUIManager.Instance.ShowDropZone(false);
        UnitManager.Instance.SpawnBomb();

        Destroy(gameObject);
    }
}
