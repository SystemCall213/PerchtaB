using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StickerShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image sticker;

    void Start()
    {
        sticker = GetComponent<Image>();
        Color c = sticker.color;
        c.a = 0;
        sticker.color = c;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color c = sticker.color;
        c.a = 1;
        sticker.color = c;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color c = sticker.color;
        c.a = 0;
        sticker.color = c;
    }
}
