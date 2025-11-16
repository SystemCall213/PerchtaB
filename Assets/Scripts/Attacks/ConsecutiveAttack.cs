using System.Collections;
using UnityEngine;

public class ConsecutiveAttack : Attack
{
    public float delayBetweenExecutingAreas;

    public override void Execute()
    {
        StartCoroutine(ExecuteAreasWithDelay());
    }

    private IEnumerator ExecuteAreasWithDelay()
    {
        foreach (AttackArea area in attackAreas)
        {
            yield return new WaitForSeconds(delayBetweenExecutingAreas);
            area.Activate();
        }
    }
}
