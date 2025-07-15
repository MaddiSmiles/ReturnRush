using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip tackleClip;
    public AudioClip whistleClip;
    public AudioClip footstepClip;
    public AudioClip dashClip;

    [Range(0f, 1f)] public float globalSFXVolume = 1f;   // 🎚️ Master SFX slider (from settings)
    [Range(0f, 1f)] public float footstepVolume = 0.5f;  // fine-tuning
    [Range(0f, 1f)] public float dashVolume = 0.9f;      // fine-tuning
    [Range(0f, 1f)] public float tackleVolume = 1f;
    [Range(0f, 1f)] public float whistleVolume = 0.7f;

    public bool isGameOver = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource != null && clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
            musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        if (clip == null || sfxSource == null || isGameOver)
            return;

        sfxSource.PlayOneShot(clip, volume * globalSFXVolume); // ✅ apply global slider
    }

    public void PlaySFX_IgnoreGameOver(AudioClip clip, float volume)
    {
        if (clip == null || sfxSource == null)
            return;

        sfxSource.PlayOneShot(clip, volume * globalSFXVolume); // ✅ apply global slider
    }

    public void PlayLoopingSFX(AudioClip clip, float volume)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.Stop(); // Stop any previous clip
            sfxSource.clip = clip;
            sfxSource.volume = volume * globalSFXVolume; // ✅ apply global slider
            sfxSource.loop = true;
            sfxSource.Play();
        }
    }

    public void StopLoopingSFX(AudioClip clip)
    {
        if (sfxSource != null && sfxSource.isPlaying && sfxSource.clip == clip)
            sfxSource.Stop();
    }

    // 🎯 Clean wrappers
    public void PlayTackleSFX() => PlaySFX_IgnoreGameOver(tackleClip, tackleVolume);
    public void PlayWhistleSFX() => PlaySFX(whistleClip, whistleVolume);
    public void PlayDashSFX() => PlaySFX(dashClip, dashVolume);
    public void PlayFootstepLoop() => PlayLoopingSFX(footstepClip, footstepVolume);
}
