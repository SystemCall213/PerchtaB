using System.Collections.Generic;

[System.Serializable]
public class DialogueLine
{
    public string id;
    public string speaker;
    public string text;
    public string nextLineId;
    public string conditionFlag;
    public List<Choice> choices;
}
