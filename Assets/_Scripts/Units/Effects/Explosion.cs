using System.Collections;
using UnityEngine;

public class Explosion : EffectUnitBase
{
    private void Start()
    {
        StartCoroutine(DestroyObject());
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
