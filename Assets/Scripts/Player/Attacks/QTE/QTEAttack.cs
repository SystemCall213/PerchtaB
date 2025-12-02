using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QTEAttack : PlayerAttack
{
    [SerializeField] QTEOverlapLayoutGroup qteSymbolsHolder;
    [SerializeField] QTESymbol symbolPrefab;

    public bool isAttacking = false;

    private readonly char[] possibleKeys = new char[] { 'W', 'A', 'S', 'D' };
    private readonly List<QTESymbol> symbols = new List<QTESymbol>();

    private int numOfSymbolsTotal = 0;
    private int correctPressedSymbols = 0;
    public float wrongButtonTimePunishment = 0.3f;

    public override void StartAttack()
    {
        if (isAttacking) return;
        numOfSymbolsTotal = 0;

        RoundManager.Instance.ToggleButtons();

        isAttacking = true;
        qteSymbolsHolder.gameObject.SetActive(true);
        circleTimer.StartTimer(10f);
        correctPressedSymbols = 0;
        qteSymbolsHolder.firstBatch = 3;

        StartCoroutine(AttackRoutine());
    }

    public override void FinishAttack()
    {
        foreach (var s in symbols)
            Destroy(s.gameObject);
        symbols.Clear();
        
        int dmg = correctPressedSymbols / 3;

        Enemy.Instance.TakeDamage(dmg);

        StartCoroutine(StartEnemyAttack());
    }

    private IEnumerator StartEnemyAttack()
    {
        yield return new WaitForSeconds(2f);

        RoundManager.Instance.Toggle();
    }

    private IEnumerator AttackRoutine()
    {
        while (!isAttacking)
            yield return null;

        SpawnSymbols();
        HighlightFirst();

        while (isAttacking)
        {
            if (symbols.Count == 0)
            {
                EndAttack();
                yield break;
            }

            char expected = symbols[0].Key;

            if (AnyWASDKeyPressed() && !CheckKeyPressed(expected))
            {
                // Wrong input -> outline red
                symbols[0].SetOutlineRed();
                circleTimer.timeLeft -= wrongButtonTimePunishment;
                if (circleTimer.timeLeft < 0)
                {
                    circleTimer.timeLeft = 0;
                    circleTimer.timerExpired.Invoke();
                    circleTimer.timerImage.fillAmount = circleTimer.timeLeft / circleTimer.maxTime;
                }
            }

            if (CheckKeyPressed(expected))
            {
                HandleCorrectKey();
            }

            yield return null;
        }
    }

    private void SpawnSymbols()
    {
        foreach (var s in symbols)
            Destroy(s.gameObject);
        symbols.Clear();

        for (int i = 0; i < 6; i++)
        {
            AddNewSymbol();
        }
    }

    private void HighlightFirst()
    {
        for (int i = 0; i < symbols.Count; i++)
        {
            if (i == 0)
                symbols[i].transform.localScale = Vector3.one * 2.75f;
            else
                symbols[i].transform.localScale = Vector3.one * 2f;
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
        first.SetOutlineGreen();
        correctPressedSymbols++;
        // Animate the hit symbol (up + left, then destroy)
        Vector3 targetPos = first.transform.localPosition + new Vector3(-50f, 60f, 0f);
        // Remove from list
        symbols.RemoveAt(0);

        LeanTween.moveLocal(first.gameObject, targetPos, 0.25f).setEaseOutQuad();
        LeanTween.scale(first.gameObject, Vector3.zero, 0.25f).setEaseInBack()
            .setOnComplete(() =>
            {
                Destroy(first.gameObject);
            });
        qteSymbolsHolder.firstBatch -= 1;
        if (qteSymbolsHolder.firstBatch == 0) qteSymbolsHolder.firstBatch = 3;
            

        // Shift remaining left (tween)
        /*
        for (int i = 0; i < symbols.Count; i++)
        {
            RectTransform rt = symbols[i].GetComponent<RectTransform>();
            Vector3 original = rt.localPosition;
            Vector3 shifted = original + new Vector3(-25f, 0, 0);

            LeanTween.moveLocal(rt.gameObject, shifted, 0.2f).setEaseOutQuad();
        }
        */

        // New first symbol should be enlarged
        AddNewSymbol();
        HighlightFirst();
    }

    private void EndAttack()
    {
        isAttacking = false;
        qteSymbolsHolder.gameObject.SetActive(false);
    }

    private void AddNewSymbol()
    {
        QTESymbol newSym = Instantiate(symbolPrefab, qteSymbolsHolder.transform);

        // Random WASD
        char key = possibleKeys[Random.Range(0, possibleKeys.Length)];
        newSym.Key = key;
        newSym.GetComponentInChildren<TextMeshProUGUI>().text = key.ToString();
        Canvas canvas = newSym.GetComponent<Canvas>();
        canvas.sortingOrder = 1000 - numOfSymbolsTotal;
        numOfSymbolsTotal++;

        symbols.Add(newSym);
    }

    private bool AnyWASDKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D);
    }
}
