using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Hover SFX")]
    public AudioClip[] hoverClips;

    int lastHoverIndex = -1;

    [Header("Panel SFX")]
    public AudioClip panelCloseSFX;
    public AudioClip panelOpenSFX;

    [Header("CollectSFX")]
    public AudioClip[] collectSFXs;

    [Header("Scene Transition SFX")]
    public AudioClip sceneTransitionSFX;

    [Header("Music Clips")]
    public AudioClip mainMenuMusic;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        PlayMusic(mainMenuMusic);
    }

    // --- SFX ---
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // --- Random Hover ---
    public void PlayRandomHover()
    {
        if (hoverClips.Length == 0) return;

        int index;

        // Prevent same sound twice in a row
        do
        {
            index = Random.Range(0, hoverClips.Length);
        }
        while (index == lastHoverIndex && hoverClips.Length > 1);

        lastHoverIndex = index;

        sfxSource.pitch = Random.Range(0.95f, 1.05f);
        sfxSource.PlayOneShot(hoverClips[index]);
        sfxSource.pitch = 1f;
    }

    // --- Panel SFX ---
    public void PlayPanelClose()
    {
        sfxSource.PlayOneShot(panelCloseSFX);
    }

    public void PlayPanelOpen()
    {
        sfxSource.PlayOneShot(panelOpenSFX);
    }

    // --- Music ---
    public void PlayMusic(AudioClip music, bool loop = true)
    {
        musicSource.clip = music;
        musicSource.loop = loop;
        musicSource.Play();
    }

    // --- Collect SFX ---
    // Plays a random collect SFX from the array and applies a random pitch variation and prevents the same sound from playing twice in a row.
    public void PlayRandomCollectSFX()
    {
        if (collectSFXs.Length == 0) return;

        int index;

        // Prevent same sound twice in a row
        do
        {
            index = Random.Range(0, collectSFXs.Length);
        }
        while (index == lastHoverIndex && collectSFXs.Length > 1);

        lastHoverIndex = index;

        sfxSource.pitch = Random.Range(0.9f, 1.1f);
        sfxSource.PlayOneShot(collectSFXs[index]);
        sfxSource.pitch = 1f;
        sfxSource.Play();
    }
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}