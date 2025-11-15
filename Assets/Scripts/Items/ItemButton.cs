using System;
using TMPro;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    private Item item;
    private Action<Item> onUseCallback;
    private TextMeshProUGUI buttonText;

    private void Awake()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UseItem()
    {
        onUseCallback?.Invoke(item);
    }

    public void SetItem(Item item, Action<Item> callback)
    {
        this.item = item;
        onUseCallback = callback;
        buttonText.text = item.itemName;
    }
}
