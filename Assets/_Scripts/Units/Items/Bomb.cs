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
        remainedRound = Stats.RemainedRound;
        fire = Stats.Fire;
        position = Stats.Position;
    }

    private void OnStateChanged(GameState newState)
    {
        if (newState == GameState.BombExplode)
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
            Destroy(gameObject);
        }
    }
}
