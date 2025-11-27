using UnityEngine;
using TMPro;

public class QTESymbol : MonoBehaviour
{
    // Stores which WASD key this symbol represents
    public char Key;

    // (Optional but recommended) shortcut to its TMP text component
    public TextMeshProUGUI textComponent;
    
    private void Awake()
    {
        // Auto-assign text component if not set manually
        if (textComponent == null)
            textComponent = GetComponentInChildren<TextMeshProUGUI>();
    }
}
