using System;
using UnityEngine;
using UnityEngine.UI;

public class CircleTimer : MonoBehaviour
{
    [SerializeField] public float timeLeft = 0;
    public float maxTime;

    public Image timerImage;

    public Action timerExpired;

    private void Awake()
    {
        timerImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            // Clamp
            if (timeLeft <= 0)
            {
                timeLeft = 0;
                timerExpired.Invoke();
            }

            // Fill amount from 1 → 0 based on time ratio
            timerImage.fillAmount = timeLeft / maxTime;
        }
    }

    // Optional: Restart timer externally
    public void StartTimer(float seconds)
    {
        maxTime = seconds;
        timeLeft = seconds;
        timerImage.enabled = true;
        timerImage.fillAmount = 1f;
    }
}
