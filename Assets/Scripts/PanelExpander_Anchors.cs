using UnityEngine;
using System.Collections;

public class PanelExpander_Anchors : MonoBehaviour
{
    public RectTransform panel;
    public float animationDuration = 4f;

    // The target offsets for expanded and collapsed states
    public Vector4 collapsedOffsets = new Vector4(300, 300, 600, 300); // left, right, top, bottom
    public Vector4 expandedOffsets = new Vector4(50, 50, 0, 100);

    private bool isExpanded = false;

    
    public void TogglePanel()
    {
        StopAllCoroutines();
        StartCoroutine(AnimatePanel(isExpanded ? collapsedOffsets : expandedOffsets));
        isExpanded = !isExpanded;
    }

    private IEnumerator AnimatePanel(Vector4 targetOffsets)
    {
        Vector4 start = new Vector4(
            panel.offsetMin.x,  // left
            panel.offsetMin.y,  // bottom
            panel.offsetMax.x,  // right
            panel.offsetMax.y   // top
        );

        float elapsed = 0f;
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animationDuration);

            float left   = Mathf.Lerp(start.x, targetOffsets.x, t);
            float bottom = Mathf.Lerp(start.y, -targetOffsets.w, t);
            float right  = Mathf.Lerp(start.z, -targetOffsets.y, t);
            float top    = Mathf.Lerp(start.w, -targetOffsets.z, t);

            panel.offsetMin = new Vector2(left, bottom);
            panel.offsetMax = new Vector2(right, top);

            yield return null;
        }

        panel.offsetMin = new Vector2(targetOffsets.x, -targetOffsets.w);
        panel.offsetMax = new Vector2(-targetOffsets.y, -targetOffsets.z);
    }
}
