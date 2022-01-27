using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// �޲z�Ҧ��a�ϮĪG�A�p���O/�r��d��A�Ѿl�^�X�k�s��Destroy
/// </summary>
public class EffectManager : StaticInstance<EffectManager>
{
    [SerializeField] GameObject explosionPrefab;

    public void SpawnEffect(Effect effect)
    {
        switch (effect)
        {
            case Effect.Fire:
                if (Helpers.DoesTagExist("Bomb"))
                {
                    var bombsOnMap = GameObject.FindGameObjectsWithTag("Bomb");

                    foreach (var bomb in bombsOnMap)
                    {
                        var b = bomb.GetComponent<Bomb>();
                        if (b.remainedRound == 0)
                        {
                            b.DestroySelf();
                            StartCoroutine(BombExplode(b));
                        }
                    }
                }
                break;
            case Effect.Poison:
                break;
            default:
                throw new ArgumentOutOfRangeException("Effect not found");
        }
    }

    private IEnumerator BombExplode(Bomb bomb)
    {
        yield return new WaitForSeconds(1);
        Instantiate(explosionPrefab, bomb.position, Quaternion.identity);
    }
}

public enum Effect
{
    Fire = 0, // ���O
    Poison = 1 // �r��
}
