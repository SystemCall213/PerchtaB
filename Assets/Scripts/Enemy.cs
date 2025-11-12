using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<AttackPattern> patterns;
    private RectTransform position;

    void Start()
    {
        position = GetComponent<RectTransform>();
    }

    public void Execute()
    {
        int index = Random.Range(0, patterns.Count);
        AttackPattern pattern = patterns[index];

        StartCoroutine(startAttack(pattern));
    }

    private IEnumerator startAttack(AttackPattern _pattern)
    {
        yield return new WaitForSeconds(2f);

        _pattern.Execute();
    }

    public RectTransform GetRectTransform()
    {
        return position;
    }
}
