using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public Sound[] sounds;
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
            sound.SetSource(gameObject.AddComponent<AudioSource>());
        }
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        sound.source.Play();
    }

    public void Stop(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        sound.source.Stop();
    }

    public void Pause(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        sound.source.Pause();
    }

    public void UnPause(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        sound.source.UnPause();
    }

    public AudioSource GetAudioSource(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        return sound.source;
    }
}
