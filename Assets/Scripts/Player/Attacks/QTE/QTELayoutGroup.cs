using UnityEngine;
using UnityEngine.UI;

public class QTEOverlapLayoutGroup : LayoutGroup
{
    [Tooltip("Multiplier of overlap relative to previous element width (0.5 = half overlap)")]
    public float overlapFactor = 0.5f;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        CalcPositions();
    }

    public override void CalculateLayoutInputVertical()
    {
        CalcPositions();
    }

    public override void SetLayoutHorizontal()
    {
        SetChildrenPositions();
    }

    public override void SetLayoutVertical()
    {
        SetChildrenPositions();
    }

    private void CalcPositions()
    {
        float x = padding.left;
        float maxHeight = 0f;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            RectTransform child = rectChildren[i];
            float w = LayoutUtility.GetPreferredWidth(child);
            float h = LayoutUtility.GetPreferredHeight(child);

            if (i == 0)
            {
                x = padding.left;
            }
            else
            {
                RectTransform prev = rectChildren[i - 1];
                float prevWidth = LayoutUtility.GetPreferredWidth(prev);
                x += prevWidth * overlapFactor;
            }

            maxHeight = Mathf.Max(maxHeight, h);
        }

        float totalWidth = x + padding.right;
        SetLayoutInputForAxis(totalWidth, totalWidth, -1, 0);
        SetLayoutInputForAxis(maxHeight + padding.top + padding.bottom, maxHeight, -1, 1);
    }

    private void SetChildrenPositions()
    {
        float x = padding.left;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            RectTransform child = rectChildren[i];
            float w = LayoutUtility.GetPreferredWidth(child);
            float h = LayoutUtility.GetPreferredHeight(child);

            if (i > 0)
            {
                RectTransform prev = rectChildren[i - 1];
                float prevWidth = LayoutUtility.GetPreferredWidth(prev);

                x += prevWidth * overlapFactor;
            }

            SetChildAlongAxis(child, 0, x, w);
            SetChildAlongAxis(child, 1, padding.top, h);
        }
    }
}
