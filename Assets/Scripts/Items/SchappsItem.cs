public class SchappsItem : Item
{
    public override void ApplyEffect()
    {
        SceneFader.Instance.FadeToScene("BadEnding");
    }

    public SchappsItem() : base("\"Schnapps\"!") {}
}
