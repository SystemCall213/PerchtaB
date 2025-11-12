using System.Collections;
using UnityEngine;

public class RockArea : AttackArea
{
    public Transform start;
    public Hand handPrefab;
    public Player player;

    public override void Activate()
    {
        StartCoroutine(Execute());
    }

    private IEnumerator Execute()
    {
        yield return new WaitForSeconds(2f);

        Hand newHand = Instantiate(handPrefab, start.position, Quaternion.identity);
        newHand.SetPlayer(player.GetTransform());
    }
}
