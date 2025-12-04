using UnityEngine;

public class PathBoundary : MonoBehaviour
{
    public bool active = false;
    public float margin = 50f;
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        if (col == null)
            Debug.LogError("PathBoundary requires a Collider2D!");
    }

    // CHECK IF A POSITION IS INSIDE THE COLLIDER
    public bool IsInside(Vector3 worldPos)
    {
        if (col == null) return false;

        // Check the point directly first
        if (col.OverlapPoint(worldPos)) return true;

        return false;
    }
}
