using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public RectTransform cursorUI;   // The UI Image RectTransform
    public Canvas canvas;            // The canvas the cursor belongs to

    void Start()
    {
        Cursor.visible = false; 
    }

    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out pos
        );

        cursorUI.anchoredPosition = pos;
    }
}
