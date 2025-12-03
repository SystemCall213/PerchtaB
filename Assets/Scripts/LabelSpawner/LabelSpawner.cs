using UnityEngine;

public class LabelSpawner : MonoBehaviour
{
    public static LabelSpawner Instance;

    public Label labelPrefab; // TextMeshPro Label prefab

    private Camera cam;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        cam = Camera.main;
    }

    public void SpawnLabel(Vector3 worldPos, string text, Color color)
    {
        // Convert world → screen → canvas position
        Vector3 screenPos = cam.WorldToScreenPoint(worldPos);

        // Instantiate label as child of LabelSpawner
        Label label = Instantiate(labelPrefab, transform);
        label.SetText(text, color);
        label.SetWorldPosition(screenPos);
    }
}
