using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBomb : CardBase
{
    public override IEnumerator UseCard()
    {
        yield return new WaitForSeconds(2);

        UnitManager.Instance.SpawnBomb();
        GUIManager.Instance.ShowDropZone(false);

        Destroy(gameObject);
    }
}
