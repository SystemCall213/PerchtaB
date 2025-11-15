using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    public List<AttackPattern> patterns;
    private RectTransform position;
    public HPBar hPBar;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

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

    public void TakeDamage(int dmg)
    {
        hPBar.TakeDmg(dmg);
    }
}
