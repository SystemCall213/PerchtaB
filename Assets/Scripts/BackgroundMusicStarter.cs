using UnityEngine;

public class BackgroundMusicStarter : MonoBehaviour
{
    public AudioClip bgMusic;
    public AudioClip bgSoundEffect;
    public float bgMusicVolume;
    public float bgSoundEffectVolume;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(bgMusic);
        AudioManager.Instance.SetMusicVolume(bgMusicVolume);

        if (bgSoundEffect)
        {
            AudioManager.Instance.PlaySoundEffectMusic(bgSoundEffect);
            AudioManager.Instance.SetSoundEffectMusicVolume(bgSoundEffectVolume);       
        }
    }
}
