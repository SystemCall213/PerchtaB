using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shnapps : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    public GameObject description;

    void Start()
    {
        image = GetComponent<Image>();

        if (PlayerFlags.Instance.HasFlag("has_schnapps"))
        {
            image.enabled = true;
        }

        description.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (PlayerFlags.Instance.HasFlag("has_schnapps"))
        {
            description.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (PlayerFlags.Instance.HasFlag("has_schnapps"))
        {
            description.SetActive(false);
        }
    }
}
