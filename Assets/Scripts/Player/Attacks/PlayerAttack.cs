using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] protected CircleTimer circleTimer;

    private void OnEnable()
    {
        circleTimer.timerExpired += FinishAttack;
    }

    void OnDisable()
    {
        circleTimer.timerExpired -= FinishAttack;
    }

    public virtual void StartAttack() {}

    public virtual void FinishAttack() {}
}
