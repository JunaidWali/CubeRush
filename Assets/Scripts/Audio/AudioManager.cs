using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public float defaultVolume = 0.3f;
    public float silencedVolume = 0.1f;

    [SerializeField] private Sound[] sounds;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Sound sound in sounds)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            sound.SetSource(audioSource);
        }
    }

    public void StopAll()
    {
        // Stop all AudioSources
        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
        {
            audioSource.Stop();
        }
    }

    public void StopAllExcept(string name)
    {
        // Stop all AudioSources except the one with the specified name
        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
        {
            if (audioSource.clip.name != name)
            {
                audioSource.Stop();
            }
        }
    }

    public void PauseAll()
    {
        // Pause all AudioSources
        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }

    public void PauseAllExcept(string name)
    {
        // Pause all AudioSources except the one with the specified name
        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
        {
            if (audioSource.isPlaying && audioSource.clip.name != name)
            {
                audioSource.Pause();
            }
        }
    }

    public void UnPauseAll()
    {
        // Unpause all AudioSources
        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
        {
            audioSource.UnPause();
        }
    }

    public AudioSource GetSource(string name)
    {
        // Get the AudioSource with the specified name
        foreach (Sound sound in sounds)
        {
            if (sound.name == name)
            {
                return sound.source;
            }
        }

        // Return null if no AudioSource is found with the specified name
        return null;
    }
}
