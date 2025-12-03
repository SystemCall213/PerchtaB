using System.Collections;
using TMPro;
using UnityEngine;

public class Label : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public CanvasGroup canvasGroup;

    private Transform tr;

    private float lifetime = 2f;
    private float riseSpeed = 1.2f; // world-space units per second

    private void Awake()
    {
        tr = transform;
    }

    public void SetText(string text, Color color)
    {
        tmp.text = text;
        tmp.color = color;

        StartCoroutine(Animate());
    }

    public void SetWorldPosition(Vector3 pos)
    {
        tr.position = pos;
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;

        while (elapsed < lifetime)
        {
            elapsed += Time.deltaTime;

            // Move upward in world space
            tr.position += Vector3.up * riseSpeed * Time.deltaTime;

            // Fade out
            canvasGroup.alpha = 1f - (elapsed / lifetime);

            yield return null;
        }

        Destroy(gameObject);
    }
}
