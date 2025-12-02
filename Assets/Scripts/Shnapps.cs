using UnityEngine;
using UnityEngine.UI;

public class Shnapps : MonoBehaviour
{
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        if (PlayerFlags.Instance.HasFlag("has_schnapps"))
        {
            image.enabled = true;
        }
    }
}
