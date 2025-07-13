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

    [Range(0f, 1f)]
    public float footstepVolumef = 0.5f; // Set default footstep volume to 50%

    [Range(0f, 1f)]
    public float dashVolume = 1f; // Set default dash volume to 100%

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

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (isGameOver || sfxSource == null || clip == null)
            return;

        sfxSource.PlayOneShot(clip, volume);
    }



    public void PlayLoopingSFX(AudioClip clip, float volume = 1f)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.Stop(); // Stop any previous clip
            sfxSource.clip = clip;
            sfxSource.volume = volume;
            sfxSource.loop = true;
            sfxSource.Play();
        }
    }


    public void StopLoopingSFX(AudioClip clip)
    {
        if (sfxSource != null && sfxSource.isPlaying && sfxSource.clip == clip)
        {
            sfxSource.Stop();
        }
    }
}
