[System.Serializable]
public class Choice
{
    public string text;
    public string nextLineId;
    // OPTIONAL: only show if player meets condition
    public string requiredFlag;   // e.g. "has_schnapps"
    public string setFlag;
}
