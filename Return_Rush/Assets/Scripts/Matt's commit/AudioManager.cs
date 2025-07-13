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

    public void PlaySFX(AudioClip clip)
    {
        if (isGameOver || sfxSource == null || clip == null)
            return;

        sfxSource.PlayOneShot(clip);
    }


    public void PlayLoopingSFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.clip = clip;
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
