using UnityEngine;
using UnityEngine.UI;

public class QTEOverlapLayoutGroup : LayoutGroup
{
    [Tooltip("Multiplier of overlap relative to previous element width (0.5 = half overlap)")]
    public float overlapFactor = 0.5f;

    [Tooltip("Extra horizontal spacing inserted between batches")]
    public float batchSpacing = 60f;

    [Tooltip("How many symbols per batch AFTER the first batch")]
    public int symbolsPerBatch = 4;

    [Tooltip("The size of the FIRST batch (e.g. 4)")]
    public int firstBatch = 4;

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

    private bool ShouldInsertGapHere(int index)
    {
        // First gap after FIRST batch
        if (index == firstBatch)
            return true;

        // After the first batch, every symbolsPerBatch group
        if (index > firstBatch)
        {
            int offset = index - firstBatch;
            if (offset > 0 && (offset % symbolsPerBatch) == 0)
                return true;
        }

        return false;
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

                // 🔥 NEW: insert gap based on batch logic
                if (ShouldInsertGapHere(i))
                    x += batchSpacing;
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

            if (i == 0)
            {
                x = padding.left;
            }
            else
            {
                RectTransform prev = rectChildren[i - 1];
                float prevWidth = LayoutUtility.GetPreferredWidth(prev);
                x += prevWidth * overlapFactor;

                // 🔥 NEW: insert gap based on batch logic
                if (ShouldInsertGapHere(i))
                    x += batchSpacing;
            }

            SetChildAlongAxis(child, 0, x, w);
            SetChildAlongAxis(child, 1, padding.top, h);
        }
    }
}
