using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private PlayerMovement movement;
    private Transform position;

    public HPBar hPBar;
    private List<Item> items;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        items = new List<Item>();
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
        hPBar.TakeDmg(1);
    }

    public void Heal(int healAmount)
    {
        hPBar.Heal(healAmount);
    }

    public void AddRandomItem()
    {
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
