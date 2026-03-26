using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    private AudioSource musicSource;
    private AudioSource sfxSource;

    [Header("Audio Library")]
    public Sound[] sounds;

    private Dictionary<string, AudioClip> soundDictionary;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Setup();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Setup()
    {
        // Create audio sources
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;

        // Build dictionary for fast lookup
        soundDictionary = new Dictionary<string, AudioClip>();

        foreach (Sound sound in sounds)
        {
            if (!soundDictionary.ContainsKey(sound.name))
            {
                soundDictionary.Add(sound.name, sound.clip);
            }
        }
    }

    //  Play Background Music
    public void PlayMusic(string name)
    {
        if (!soundDictionary.ContainsKey(name))
        {
            Debug.LogWarning("Music not found: " + name);
            return;
        }

        musicSource.clip = soundDictionary[name];
        musicSource.Play();
    }

    //  Stop Music
    public void StopMusic()
    {
        musicSource.Stop();
    }

    //  Play SFX
    public void PlaySFX(string name)
    {
        if (!soundDictionary.ContainsKey(name))
        {
            Debug.LogWarning("SFX not found: " + name);
            return;
        }

        sfxSource.PlayOneShot(soundDictionary[name]);
    }

    //  Play SFX with volume control
    public void PlaySFX(string name, float volume)
    {
        if (!soundDictionary.ContainsKey(name))
        {
            Debug.LogWarning("SFX not found: " + name);
            return;
        }

        sfxSource.PlayOneShot(soundDictionary[name], volume);
    }
}