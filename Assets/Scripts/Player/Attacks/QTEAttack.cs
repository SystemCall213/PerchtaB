using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QTEAttack : MonoBehaviour
{
    [SerializeField] QTEOverlapLayoutGroup qteSymbolsHolder;
    [SerializeField] QTESymbol symbolPrefab;

    public bool isAttacking = false;

    private readonly char[] possibleKeys = new char[] { 'W', 'A', 'S', 'D' };
    private readonly List<QTESymbol> symbols = new List<QTESymbol>();

    public void StartAttack()
    {
        if (isAttacking) return;

        isAttacking = true;
        qteSymbolsHolder.gameObject.SetActive(true);

        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        // Wait until isAttacking is true (your requirement)
        while (!isAttacking)
            yield return null;

        SpawnSymbols();
        HighlightFirst();

        // Input loop
        while (isAttacking)
        {
            if (symbols.Count == 0)
            {
                EndAttack();
                yield break;
            }

            char expected = symbols[0].Key;

            if (CheckKeyPressed(expected))
            {
                HandleCorrectKey();
            }

            yield return null;
        }
    }

    private void SpawnSymbols()
    {
        // Clear old ones
        foreach (var s in symbols)
            Destroy(s.gameObject);
        symbols.Clear();

        // Spawn 8 new
        for (int i = 0; i < 8; i++)
        {
            QTESymbol newSym = Instantiate(symbolPrefab, qteSymbolsHolder.transform);

            // Random WASD
            char key = possibleKeys[Random.Range(0, possibleKeys.Length)];
            newSym.Key = key;
            newSym.GetComponentInChildren<TextMeshProUGUI>().text = key.ToString();

            symbols.Add(newSym);
        }
    }

    // Scales first symbol ×1.5 and resets others to normal
    private void HighlightFirst()
    {
        for (int i = 0; i < symbols.Count; i++)
        {
            if (i == 0)
                symbols[i].transform.localScale = Vector3.one * 3.75f;
            else
                symbols[i].transform.localScale = Vector3.one * 3f;
        }
    }

    private bool CheckKeyPressed(char c)
    {
        switch (c)
        {
            case 'W': return Input.GetKeyDown(KeyCode.W);
            case 'A': return Input.GetKeyDown(KeyCode.A);
            case 'S': return Input.GetKeyDown(KeyCode.S);
            case 'D': return Input.GetKeyDown(KeyCode.D);
        }
        return false;
    }

    private void HandleCorrectKey()
    {
        QTESymbol first = symbols[0];

        // Animate the hit symbol (up + left, then destroy)
        Vector3 targetPos = first.transform.localPosition + new Vector3(-50f, 60f, 0f);

        LeanTween.moveLocal(first.gameObject, targetPos, 0.25f).setEaseOutQuad();
        LeanTween.scale(first.gameObject, Vector3.zero, 0.25f).setEaseInBack()
            .setOnComplete(() =>
            {
                Destroy(first.gameObject);
            });

        // Remove from list
        symbols.RemoveAt(0);

        // Shift remaining left (tween)
        for (int i = 0; i < symbols.Count; i++)
        {
            RectTransform rt = symbols[i].GetComponent<RectTransform>();
            Vector3 original = rt.localPosition;
            Vector3 shifted = original + new Vector3(-25f, 0, 0);

            LeanTween.moveLocal(rt.gameObject, shifted, 0.2f).setEaseOutQuad();
        }

        // New first symbol should be enlarged
        HighlightFirst();
    }

    private void EndAttack()
    {
        isAttacking = false;
        qteSymbolsHolder.gameObject.SetActive(false);
    }
}
