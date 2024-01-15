using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public float levelThemeVolume = 0.3f;
    public float levelThemeVolumeSilenced = 0.1f;

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

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        sound.source.Play();
    }

    public void PlayDelayed(string name, float delay)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        sound.source.PlayDelayed(delay);
    }

    public void Stop(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        sound.source.Stop();
    }

    public void StopAll()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.Stop();
        }
    }

    public void StopAllExcept(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name != name)
            {
                sound.source.Stop();
            }
        }
    }

    public void Pause(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        sound.source.Pause();
    }

    public void PauseAll()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.Pause();
        }
    }

    public void PauseAllExcept(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name != name)
            {
                sound.source.Pause();
            }
        }
    }

    public void UnPause(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        sound.source.UnPause();
    }

    public void UnPauseAll()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.UnPause();
        }
    }

    public void SetVolume(string name, float volume)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        sound.source.volume = volume;
    }

    public void SetPitch(string name, float pitch)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        sound.source.pitch = pitch;
    }

    public bool IsPlaying(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        return sound.source.isPlaying;
    }

    public AudioSource GetAudioSource(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        return sound.source;
    }
}
