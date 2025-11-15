using UnityEngine;
using UnityEngine.UI;

public class HPPoint : MonoBehaviour
{
    public Sprite sprite;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = sprite;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
