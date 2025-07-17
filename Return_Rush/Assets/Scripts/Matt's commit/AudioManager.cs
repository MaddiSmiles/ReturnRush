using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip BGMusic;
    public AudioClip tackle;
    public AudioClip whistle;
    public AudioClip steps;
    public AudioClip dash;

    private void Start()
    {
        musicSource.clip = BGMusic;
        musicSource.Play();

    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);

    }



}

