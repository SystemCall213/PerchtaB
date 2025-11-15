using TMPro;
using UnityEngine;

public class BattleText : MonoBehaviour
{
    public static BattleText Instance;
    private TextMeshProUGUI textHolder;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        textHolder = GetComponent<TextMeshProUGUI>();
        SetText("dewdawefse");
    }

    public void SetText(string text)
    {
        textHolder.text = text;
    }

    public void Disable()
    {
        textHolder.enabled = false;
    }

    public void Enable()
    {
        textHolder.enabled = true;
    }

}
