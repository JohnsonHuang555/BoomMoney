using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTest : CardBase
{
    public override IEnumerator UseCard()
    {
        yield return new WaitForSeconds(2);
        GUIManager.Instance.ShowDropZone(false);

        Debug.Log("You did it Test....");

        Destroy(gameObject);
    }
}
