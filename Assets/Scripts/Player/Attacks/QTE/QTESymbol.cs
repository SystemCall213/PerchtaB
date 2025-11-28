using UnityEngine;
using TMPro;
using UnityEngine.UI;   // <- Needed for Image

public class QTESymbol : MonoBehaviour
{
    public char Key;
    public TextMeshProUGUI textComponent;
    public Image outline;
    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        if (textComponent == null)
            textComponent = GetComponentInChildren<TextMeshProUGUI>();

        if (outline != null)
            outline.color = new Color(0, 0, 0, 0);
    }

    public void SetOutlineGreen()
    {
        if (outline != null)
            outline.color = Color.green;
    }

    public void SetOutlineRed()
    {
        if (outline != null)
            outline.color = Color.red;
    }

    public void ClearOutline()
    {
        if (outline != null)
            outline.color = new Color(0, 0, 0, 0);
    }
}
