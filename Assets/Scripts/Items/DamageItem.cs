public class DamageItem : Item
{
    private int dmgAmount = 5;

    public override void ApplyEffect()
    {
        Enemy.Instance.TakeDamage(dmgAmount);
    }

    public DamageItem() : base("\"deal 5 damage Item\"!") {}
}
