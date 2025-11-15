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
        RoundManager.Instance.ToggleButtons();

        text.Disable();
        List<Item> items = Player.Instance.GetItems();

        foreach (Item item in items)
        {
            ItemButton button = Instantiate(itemButtonPrefab, itemsHolder.transform);

            button.SetItem(item, OnItemUsed);
        }
    }

    private void OnItemUsed(Item item)
    {
        text.Enable();
        text.SetText($"Used: {item.itemName}");

        item.ApplyEffect();

        Player.Instance.GetItems().Remove(item);
        foreach (Transform child in itemsHolder.transform)
        {
            Destroy(child.gameObject);
        }

        StartCoroutine(StartRound());
    }

    private IEnumerator StartRound()
    {
        yield return new WaitForSeconds(2f);

        RoundManager.Instance.Toggle();
    }
}
