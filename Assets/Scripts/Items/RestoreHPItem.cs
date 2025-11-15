public class RestoreHPItem : Item
{
    private int healAmount = 5;

    public override void ApplyEffect()
    {
        Player.Instance.Heal(healAmount);
    }

    public RestoreHPItem() : base("\"heal 5 hp Item\"!") {}
}
