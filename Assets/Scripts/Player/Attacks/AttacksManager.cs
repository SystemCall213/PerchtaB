using UnityEngine;

public class AttacksManager : MonoBehaviour
{
    [SerializeField] private JITAttackHolder jITAttackHolder;
    [SerializeField] private AttackFill attackFill;
    [SerializeField] private QTEAttack qTEAttack;

    public void ChooseAndStartAttack()
    {
        BattleText.Instance.SetText("");
        int maxHp = Enemy.Instance.hPBar.MaxHp();
        int currentHp = Enemy.Instance.hPBar.CurrentHp();

        float percent = (float) currentHp / maxHp;

        if (percent > 0.66) qTEAttack.StartAttack();
        else if (percent > 0.33) jITAttackHolder.StartAttack();
        else attackFill.StartAttack();
    }
    
}
