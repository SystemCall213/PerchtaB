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
    public GameObject itemsContainer;

    public void UseItems()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClicked);
        if (Player.Instance.GetItems().Count > 0)
        {
            foreach (ItemButton itemButton in itemsHolder.GetComponentsInChildren<ItemButton>())
            {
                Destroy(itemButton.gameObject);
            }

            itemsContainer.SetActive(true);
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
        itemsContainer.SetActive(false);
        text.Enable();
        text.SetText($"Used: {item.itemName}");

        Player.Instance.GetItems().Remove(item);
        foreach (Transform child in itemsHolder.transform)
        {
            Destroy(child.gameObject);
        }

        item.ApplyEffect();

        if (item is not SchappsItem)
        {
            StartCoroutine(StartRound());   
        }
    }

    private IEnumerator StartRound()
    {
        yield return new WaitForSeconds(2f);

        RoundManager.Instance.Toggle();
    }

    public void BackPressed()
    {
        itemsContainer.SetActive(false);
        RoundManager.Instance.ToggleButtons();
    }
}
