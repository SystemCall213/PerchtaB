using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UseItemManager : MonoBehaviour
{
    public ItemButton itemButtonPrefab;
    public BattleText text;
    public HorizontalLayoutGroup itemsHolder;

    public void UseItems()
    {
        if (Player.Instance.GetItems().Count > 0)
        {
            RoundManager.Instance.ToggleButtons();

            text.Disable();
            List<Item> items = Player.Instance.GetItems();

            foreach (Item item in items)
            {
                ItemButton button = Instantiate(itemButtonPrefab, itemsHolder.transform);

                button.SetItem(item, OnItemUsed);
            }   
        }
        else
        {
            BattleText.Instance.SetText("You don't have any items!");
        }
    }

    private void OnItemUsed(Item item)
    {
        text.Enable();
        text.SetText($"Used: {item.itemName}");

        Player.Instance.GetItems().Remove(item);
        foreach (Transform child in itemsHolder.transform)
        {
            Destroy(child.gameObject);
        }

        item.ApplyEffect();

        StartCoroutine(StartRound());
    }

    private IEnumerator StartRound()
    {
        yield return new WaitForSeconds(2f);

        RoundManager.Instance.Toggle();
    }
}
