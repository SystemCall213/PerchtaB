using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StickerShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image sticker;
    public bool isActive = false;
    public string sceneToTransitionTo;
    public static StickerShow Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        sticker = GetComponent<Image>();
        Color c = sticker.color;
        c.a = 0;
        sticker.color = c;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isActive)
        {
            Color c = sticker.color;
            c.a = 1;
            sticker.color = c;   
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isActive)
        {
            Color c = sticker.color;
            c.a = 0;
            sticker.color = c;   
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isActive) SceneFader.Instance.FadeToScene(sceneToTransitionTo);
    }

    public void SetTransitionActive(bool _isActive)
    {
        isActive = _isActive;
    }
}
