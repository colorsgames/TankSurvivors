using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public static bool soundPause;

    [SerializeField] private AudioClip buttonCick;

    [SerializeField] private AudioSource sourceUI;
    [SerializeField] private AudioSource menuMusic;
    [SerializeField] private AudioSource introGPMusic;
    [SerializeField] private AudioSource GPMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (MainMenuManager.isMenu)
        {
            if (!menuMusic.isPlaying)
            {
                introGPMusic.Stop();
                GPMusic.Stop();
                menuMusic.Play();
            }
        }
        else
        {
            if (menuMusic.isPlaying)
            {
                menuMusic.Stop();
                if (!introGPMusic.isPlaying)
                    introGPMusic.Play();
            }

            if (!introGPMusic.isPlaying && !GPMusic.isPlaying)
            {
                introGPMusic.Stop();
                GPMusic.Play();
            }
        }
    }

    public void PlayButtonClicSound()
    {
        sourceUI.PlayOneShot(buttonCick);
    }

    public void PlayAudioClip(AudioClip clip)
    {
        sourceUI.PlayOneShot(clip);
    }

    public void ADVSoundStop()
    {
        if (!soundPause)
        {
            AudioListener.pause = true;
        }
    }

    public void ADVSoundReturn()
    {
        if (!soundPause)
        {
            AudioListener.pause = false;
        }
    }
}
