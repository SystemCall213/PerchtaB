using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class AttackFill : MonoBehaviour
{
    public RectTransform fillImage;
    public GameObject attackImage;
    public AttackTimer timer;
    public Enemy enemy;

    [Header("Fill Settings")]
    public float fill = 0f;          // 0 = empty, 100 = full
    public float increaseAmount = 4f;
    public float decreaseAmount = 1f;
    public float decreaseInterval = 0.1f;

    private float decreaseTimer = 0f;
    private bool attackImageActive = false;

    void OnEnable()
    {
        timer.OnCountdownFinished += FinishAttack;
    }

    void OnDisable()
    {
        timer.OnCountdownFinished -= FinishAttack;
    }

    public void StartAttack()
    {
        RoundManager.Instance.ToggleButtons();
        timer.enabled = true;
        timer.StartAttack();
        attackImage.SetActive(true);
        attackImageActive = true;
        SetFill(0);
    }

    private void Update()
    {
        if (attackImageActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                fill += increaseAmount;
                fill = Mathf.Clamp(fill, 0f, 100f);
                SetFill(fill);
                decreaseTimer = 0f; // reset decrease timer
            }

            decreaseTimer += Time.deltaTime;
            if (decreaseTimer >= decreaseInterval)
            {
                decreaseTimer = 0f;

                fill -= decreaseAmount;
                fill = Mathf.Clamp(fill, 0f, 100f);
                SetFill(fill);
            }   
        }
    }

    public void SetFill(float value)
    {
        float y = value - 100f;
        fillImage.localPosition = new Vector3(fillImage.localPosition.x, y, fillImage.localPosition.z);
    }

    private void FinishAttack()
    {
        attackImage.SetActive(false);
        attackImageActive = false;

        int dmg = (int)(fill / 20);

        enemy.TakeDamage(dmg);

        fill = 0;

        StartCoroutine(StartEnemyAttack());
    }

    private IEnumerator StartEnemyAttack()
    {
        yield return new WaitForSeconds(2f);

        RoundManager.Instance.Toggle();
    }
}
