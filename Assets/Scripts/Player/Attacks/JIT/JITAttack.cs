using System;
using UnityEngine;

public class JITAttack : PlayerAttack
{
    [Header("UI References")]
    public RectTransform marker;
    public RectTransform bar;
    public JITHitZone oneDmgHitZone;
    public JITHitZone twoDmgHitZone;

    [Header("Settings")]
    public KeyCode attackKey = KeyCode.Space;
    public float baseSpeed = 800f;      // starting movement speed
    public float speedIncrease = 40f;   // added every successful hit

    private float currentSpeed;
    private bool movingRight = true;
    private bool attacking = false;
    public Action attackFinished;
    private int currentDmg = 0;

    void OnEnable()
    {
        oneDmgHitZone.inTheZone += ZoneInteraction;
        twoDmgHitZone.inTheZone += ZoneInteraction;
    }

    void OnDisable()
    {
        circleTimer.timerExpired -= FinishAttack;
        oneDmgHitZone.inTheZone -= ZoneInteraction;
        twoDmgHitZone.inTheZone -= ZoneInteraction;
    }

    private void Update()
    {
        if (!attacking) return;

        MoveMarker();

        if (Input.GetKeyDown(attackKey))
            CheckHit();
    }

    public override void StartAttack()
    {
        attacking = true;
        currentSpeed = baseSpeed;
        circleTimer.StartTimer(10f); // 10 seconds attack window
        RoundManager.Instance.ToggleButtons();
        
        // Start marker on left side
        marker.anchoredPosition = new Vector2(-bar.rect.width / 2f, 0);
    }

    public override void FinishAttack()
    {
        if (attacking)
        {
            circleTimer.timeLeft = 0;
            attacking = false;
            attackFinished.Invoke();
        }
    }

    // -------------------- MARKER MOVEMENT ---------------------

    private void MoveMarker()
    {
        float dir = movingRight ? 1f : -1f;
        marker.anchoredPosition += new Vector2(dir * currentSpeed * Time.deltaTime, 0);

        float leftLimit = -bar.rect.width / 2f;
        float rightLimit = bar.rect.width / 2f;

        if (marker.anchoredPosition.x >= rightLimit)
        {
            marker.anchoredPosition = new Vector2(rightLimit, 0);
            movingRight = false;
        }
        else if (marker.anchoredPosition.x <= leftLimit)
        {
            marker.anchoredPosition = new Vector2(leftLimit, 0);
            movingRight = true;
        }
    }

    // -------------------- HIT LOGIC ---------------------

    private void CheckHit()
    {
        if (currentDmg > 0)
        {
            Enemy.Instance.TakeDamage(currentDmg);
            return;
        }
        FinishAttack();
    }

    private void ZoneInteraction(int dmg)
    {
        print(dmg);
        currentDmg = dmg;
    }
}
