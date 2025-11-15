public abstract class Item
{
    public string itemName;
    public abstract void ApplyEffect();

    public Item(string name)
    {
        itemName = name;
    }
}
