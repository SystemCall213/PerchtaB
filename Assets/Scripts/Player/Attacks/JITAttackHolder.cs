using System.Collections;
using UnityEngine;

public class JITAttackHolder : MonoBehaviour
{
    [SerializeField] JITAttack jITAttack;

    void OnEnable()
    {
        Enemy.Instance.dead += DisableAttack;
        jITAttack.attackFinished += FinishAttack;
    }

    void OnDisable()
    {
        Enemy.Instance.dead -= DisableAttack;
        jITAttack.attackFinished -= FinishAttack;
    }

    public void StartAttack()
    {
        jITAttack.gameObject.SetActive(true);
        jITAttack.StartAttack();
    }

    public void FinishAttack()
    {
        jITAttack.gameObject.SetActive(false);

        StartCoroutine(StartEnemyAttack());
    }

    private IEnumerator StartEnemyAttack()
    {
        yield return new WaitForSeconds(2f);

        RoundManager.Instance.Toggle();
    }

    private void DisableAttack()
    {
        jITAttack.gameObject.SetActive(false);
    }
}
