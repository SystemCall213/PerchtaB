using System;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public static ItemFactory Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private Type[] itemTypes = new Type[]
    {
        typeof(RestoreHPItem),
        typeof(DamageItem)
    };

    public Item CreateRandomItem()
    {
        int index = UnityEngine.Random.Range(0, itemTypes.Length);
        Type selectedType = itemTypes[index];

        Item newItem = (Item)Activator.CreateInstance(selectedType);
        return newItem;
    }
}
