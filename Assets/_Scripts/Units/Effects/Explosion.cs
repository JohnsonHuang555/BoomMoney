using System.Collections;
using UnityEngine;

public class Explosion : UnitBase
{
    private void Start()
    {
        StartCoroutine(DestroyObject());
    }

    private IEnumerator DestroyObject()
    {
        // FIXME: ��Ƽȩw
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
