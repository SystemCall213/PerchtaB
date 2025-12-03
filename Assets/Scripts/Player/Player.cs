using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private PlayerMovement movement;
    private Transform position;

    public HPBar hPBar;
    private List<Item> items;
    private bool alreadyDead = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        items = new List<Item>();
    }

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        position = GetComponent<Transform>();
    }

    public Transform GetTransform()
    {
        return position;
    }

    public PlayerMovement GetPlayerMovement()
    {
        return movement;
    }

    public void TakeDamage()
    {
        int currentHp = hPBar.TakeDmg(1);
        if (currentHp == 0 && !alreadyDead)
        {
            alreadyDead = true;
            DeathScreen.Instance.Death();
        }
    }

    public void Heal(int healAmount)
    {
        hPBar.Heal(healAmount);
    }

    public void AddRandomItem()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClicked);
        RoundManager.Instance.ToggleButtons();

        Item item = ItemFactory.Instance.CreateRandomItem();
        items.Add(item);

        BattleText.Instance.SetText("You found " + item.itemName);

        StartCoroutine(StartRound());
    }

    private IEnumerator StartRound()
    {
        yield return new WaitForSeconds(2f);

        RoundManager.Instance.Toggle();
    }
    
    public List<Item> GetItems()
    {
        return items;
    }
}
