using UnityEngine;

public class Bomb : ItemUnitBase
{
    // 剩餘回合
    public int remainedRound;

    // 火力
    public int fire;

    // 位置
    public Vector2 position;

    public override void Awake() => GameManager.OnBeforeStateChanged += OnStateChanged;

    public override void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChanged;

    private void Start()
    {
        // FIXME: 不能寫死，值來自玩家出的卡片給的值
        remainedRound = 2;
        fire = 1;
        position = Stats.Position;
    }

    private void OnStateChanged(GameState newState)
    {
        if (newState == GameState.BombExplode && GameManager.Instance.isNewRound)
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

    // 外部方法需要呼叫來移除
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
