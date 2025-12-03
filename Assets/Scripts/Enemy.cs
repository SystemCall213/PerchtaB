using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    public List<AttackPattern> patterns;
    private RectTransform position;
    public HPBar hPBar;
    public System.Action dead;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        position = GetComponent<RectTransform>();
        if (PlayerFlags.Instance.HasFlag("battle_easier"))
        {
            hPBar.TakeDmg(3);
            PlayerFlags.Instance.RemoveFlag("battle_easier");
        }
    }

    public void Execute()
    {
        int index = Random.Range(0, patterns.Count);
        AttackPattern pattern = patterns[index];
        BattleText.Instance.SetText(pattern.battleText);

        StartCoroutine(startAttack(pattern));
    }

    private IEnumerator startAttack(AttackPattern _pattern)
    {
        yield return new WaitForSeconds(2f);

        _pattern.Execute();
    }

    public RectTransform GetRectTransform()
    {
        return position;
    }

    public void TakeDamage(int dmg)
    {
        for (int i = 0; i < dmg; i++)
        {
            int currentHp = hPBar.TakeDmg(1);
            if (currentHp == 0)
            {
                StopAllCoroutines();

                dead.Invoke();

                GetComponent<Image>().enabled = false;

                int playerHp = Player.Instance.hPBar.CurrentHp();
                int playerMaxHp = Player.Instance.hPBar.MaxHp();

                bool shnapps_found = ((float) playerHp / playerMaxHp) > 0.25f;

                if (shnapps_found)
                {
                    PlayerFlags.Instance.SetFlag("has_schnapps");
                }
                
                string shnapps_text = shnapps_found ? "and found Shnapps" : "";

                BattleText.Instance.SetText($"You have defeated the enemy {shnapps_text}");

                RoundManager.Instance.GetButtons().gameObject.SetActive(false);
                ButtonTransitionScene.Instance.gameObject.SetActive(true);

                break;
            }
        }
    }
}
