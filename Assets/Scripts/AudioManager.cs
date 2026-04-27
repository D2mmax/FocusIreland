using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("SFX")]
    public AudioClip[] dialogueClips;

    [Header("CollectSFX")]
    public AudioClip[] collectSFXs;

    [Header("Scene Transition SFX")]
    public AudioClip sceneTransitionSFX;

    [Header("Music Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip schoolSceneMusic;
    public AudioClip outdoorSceneMusic;
    public AudioClip shelterSceneMusic;

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


    // --- Music ---
    public void PlayMusic(AudioClip music, bool loop = true)
    {
        musicSource.clip = music;
        musicSource.loop = loop;
        musicSource.Play();
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