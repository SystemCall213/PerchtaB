using System.Collections;
using TMPro;
using UnityEngine;

public class AttackTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // assign your TextMeshProUGUI
    public int startTime = 5;  // starting value of the countdown

    public delegate void CountdownFinished();
    public event CountdownFinished OnCountdownFinished;

    public void StartAttack()
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        int time = startTime;

        while (time >= 0)
        {
            timerText.text = time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
        }

        OnCountdownFinished?.Invoke();
        timerText.text = "";
    }

    private void CountdownFinishedAction()
    {
        Debug.Log("Countdown finished!");
    }
}
