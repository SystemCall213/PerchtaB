using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool isActive = false;
    public float rotationSpeed = 180f; // degrees per second

    private SpriteRenderer sr;
    private Color baseColor;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        baseColor = sr.color;
    }

    private void Update()
    {
        if (isActive)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    public void MarkReached()
    {
        sr.color = Color.green;
        isActive = false;
    }

    public void Activate()
    {
        sr.color = baseColor;
        isActive = true;
    }

    public void Reset()
    {
        sr.color = baseColor;
        isActive = false;
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetMouseButton(0) && isActive)
        {
            PathDrawer drawer = FindObjectOfType<PathDrawer>();
            drawer.HitCheckpoint(this);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetMouseButton(0) && isActive)
        {
            PathDrawer drawer = FindObjectOfType<PathDrawer>();
            drawer.HitCheckpoint(this);
        }
    }
}
