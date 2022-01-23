using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �޲z�Ҧ��a�ϮĪG�A�p���O/�r��d��A�Ѿl�^�X�k�s��Destroy
/// </summary>
public class EffectManager : StaticInstance<EffectManager>
{
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
                        Debug.Log(bomb.GetComponent<Bomb>().fire);
                    }
                }
                break;
            case Effect.Poison:
                break;
            default:
                throw new ArgumentOutOfRangeException("Effect not found");
        }
    }
}

public enum Effect
{
    Fire = 0, // ���O
    Poison = 1 // �r��
}
