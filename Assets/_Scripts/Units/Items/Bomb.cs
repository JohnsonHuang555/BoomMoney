using UnityEngine;

public class Bomb : ItemUnitBase
{
    // �Ѿl�^�X
    public int remainedRound;

    // ���O
    public int fire;

    // ��m
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
