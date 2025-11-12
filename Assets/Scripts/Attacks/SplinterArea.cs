using System.Collections;
using UnityEngine;

public class SplinterArea : AttackArea
{
    public Transform start;
    public Transform end;

    private SpriteRenderer spriteRenderer;
    public Splinter splinterPrefab;

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
        yield return new WaitForSeconds(2f);

        spriteRenderer.enabled = false;

        Splinter newSplinter = Instantiate(splinterPrefab, start.position, Quaternion.identity);
        newSplinter.Move(end);
    }
}
