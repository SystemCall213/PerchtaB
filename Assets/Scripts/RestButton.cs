using System.Collections;
using UnityEngine;

public class RestButton : MonoBehaviour
{
    public int healAmount = 5;
    public bool restUsed = false;

    public void Rest()
    {
        if (!restUsed)
        {
            Player.Instance.Heal(healAmount);
            RoundManager.Instance.ToggleButtons();
            BattleText.Instance.SetText("You healed 5 hp!");
            StartCoroutine(StartRound());
            restUsed = true;
        }
    }

    private IEnumerator StartRound()
    {
        yield return new WaitForSeconds(2f);

        RoundManager.Instance.Toggle();
    }
}
