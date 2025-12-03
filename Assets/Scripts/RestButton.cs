using System.Collections;
using UnityEngine;

public class RestButton : MonoBehaviour
{
    public int healAmount = 5;
    public bool restUsed = false;

    public void Rest()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClicked);
        if (!restUsed)
        {
            if (Player.Instance.hPBar.CurrentHp() < Player.Instance.hPBar.MaxHp())
            {
                Player.Instance.Heal(healAmount);
                RoundManager.Instance.ToggleButtons();
                BattleText.Instance.SetText("You healed 5 hp!");
                StartCoroutine(StartRound());
                restUsed = true;   
            }
            else
            {
                BattleText.Instance.SetText("You have full hp!");
            }
        }
    }

    private IEnumerator StartRound()
    {
        yield return new WaitForSeconds(2f);

        RoundManager.Instance.Toggle();
    }
}
