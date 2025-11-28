using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPattern : MonoBehaviour
{
    private List<Attack> attacks = new List<Attack>();
    public float timeBetweenAttacks = 3f;

    public float timeBuffer = 5f;
    public string battleText = "";

    void Start()
    {
        attacks = new List<Attack>(GetComponentsInChildren<Attack>());
    }

    public void Execute()
    {
        StartCoroutine(PerformAttack());
    }
    
    private IEnumerator PerformAttack()
    {
        foreach (Attack attack in attacks)
        {
            attack.Execute();
            yield return new WaitForSeconds(timeBetweenAttacks);
        }

        yield return new WaitForSeconds(timeBuffer);
        RoundManager.Instance.Toggle();
    }
}
