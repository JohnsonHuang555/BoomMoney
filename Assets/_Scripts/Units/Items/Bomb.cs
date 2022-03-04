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
        // FIXME: ����g���A�ȨӦ۪��a�X���d��������
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

    // �~����k�ݭn�I�s�Ӳ���
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
