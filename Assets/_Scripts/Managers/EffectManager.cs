using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// 管理所有地圖效果，如火力/毒氣範圍，剩餘回合歸零後Destroy
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
    Fire = 0, // 火力
    Poison = 1 // 毒氣
}
