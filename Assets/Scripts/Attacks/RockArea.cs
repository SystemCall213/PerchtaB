using System.Collections;
using UnityEngine;

public class RockArea : AttackArea
{
    public Transform start;
    public Transform end;

    private SpriteRenderer spriteRenderer;
    public Rock rockPrefab;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Activate()
    {
        spriteRenderer.enabled = true;
        StartCoroutine(Execute());
    }

    private IEnumerator Execute()
    {
        yield return new WaitForSeconds(1f);

        Rock newRock = Instantiate(rockPrefab, start.position, Quaternion.identity);
        newRock.Move(end);

        yield return new WaitForSeconds(1.5f);

        spriteRenderer.enabled = false;
    }
}
