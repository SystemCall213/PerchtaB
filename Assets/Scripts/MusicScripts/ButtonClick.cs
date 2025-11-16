using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public AudioClip clicked;

    public void Play()
    {
        AudioManager.Instance.PlaySFX(clicked);
    }
}
