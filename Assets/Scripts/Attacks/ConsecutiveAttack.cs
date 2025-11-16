using System.Collections;
using UnityEngine;

public class ConsecutiveAttack : Attack
{
    public float delayBetweenExecutingAreas;
    public float numOfRounds;
    public float delayBetweenRounds;

    public override void Execute()
    {
        StartCoroutine(ExecuteAreasWithDelay());
    }

    private IEnumerator ExecuteAreasWithDelay()
    {
        for (int i = 0; i < numOfRounds; i++)
        {
            foreach (AttackArea area in attackAreas)
            {
                yield return new WaitForSeconds(delayBetweenExecutingAreas);
                area.Activate();
            }   
            yield return new WaitForSeconds(delayBetweenRounds);
        }
    }
}
