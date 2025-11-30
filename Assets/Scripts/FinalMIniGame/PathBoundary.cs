using UnityEngine;

public class PathBoundary : MonoBehaviour
{
    public bool active = false;

    private void OnTriggerExit2D(Collider2D col)
    {
        if (active)
        {
            PathDrawer drawer = FindObjectOfType<PathDrawer>();
            drawer.ResetDrawing();
        }
    }
}
