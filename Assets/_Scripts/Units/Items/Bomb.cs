using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : ItemUnitBase
{
    // �Ѿl�^�X
    int remainedRound;
    // ���O
    int fire;

    public override void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

    public override void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;

    private void Start()
    {
        remainedRound = Stats.RemainedRound;
        fire = Stats.Fire;
    }

    private void OnStateChanged(GameState newState)
    {
        if (newState == GameState.PlayerRoundTime)
        {
            DecreaseRemainedRound();
        }
    }

    public void IncreaseRemainedRound()
    {
        remainedRound++;
    }

    public void DecreaseRemainedRound()
    {
        remainedRound--;
    }

    private void Update()
    {
        if (remainedRound == 0)
        {
            // TODO: �I�s���ˮ`����k
            Destroy(gameObject);
        }
    }
}
