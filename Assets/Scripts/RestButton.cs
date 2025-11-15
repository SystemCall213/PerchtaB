using System.Collections;
using UnityEngine;

public class RestButton : MonoBehaviour
{
    public int healAmount = 5;

    public void Rest()
    {
        Player.Instance.Heal(healAmount);
        RoundManager.Instance.ToggleButtons();
        StartCoroutine(StartRound());
    }

    private IEnumerator StartRound()
    {
        yield return new WaitForSeconds(2f);

        RoundManager.Instance.Toggle();
    }
}
