using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource backgroundSoundEffect;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    [SerializeField] public AudioClip buttonClicked;
    [SerializeField] public AudioClip girlAttacking;
    [SerializeField] public AudioClip girlGettingDamage;
    [SerializeField] public AudioClip secondRoomTickling;
    [SerializeField] public AudioClip thirdRoomWind;

    [Header("Settings")]
    [Range(0f, 1f)]
    public float musicVolume = 1f;
    [Range(0f, 1f)]
    public float soundEffectVolume = 1f;
    [Range(0f, 1f)]
    public float sfxVolume = 1f;
    public float masterVolumeMultiplier = 1f;

    private void Awake()
    {
        // Simple singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // --- MUSIC ---
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        backgroundMusicSource.clip = clip;
        backgroundMusicSource.loop = loop;
        backgroundMusicSource.volume = musicVolume * masterVolumeMultiplier;
        backgroundMusicSource.Play();
    }

    public void PlaySoundEffectMusic(AudioClip clip, bool loop = true)
    {
        backgroundSoundEffect.clip = clip;
        backgroundSoundEffect.loop = loop;
        backgroundSoundEffect.volume = soundEffectVolume * masterVolumeMultiplier;
        backgroundSoundEffect.Play();
    }

    public void StopMusic()
    {
        backgroundMusicSource.Stop();
    }

    public void StopSoundEffectMusic()
    {
        backgroundSoundEffect.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        backgroundMusicSource.volume = volume * masterVolumeMultiplier;
    }

    public void SetSoundEffectMusicVolume(float volume)
    {
        soundEffectVolume = volume;
        backgroundSoundEffect.volume = volume * masterVolumeMultiplier;
    }

    // --- SFX ---
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, sfxVolume * masterVolumeMultiplier);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
    }

    public void UpdateVolume()
    {
        SetMusicVolume(musicVolume);
        SetSoundEffectMusicVolume(soundEffectVolume);
        SetSFXVolume(sfxVolume);
    }
}

