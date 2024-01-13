using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name = "New Sound";
    public AudioClip clip;
    public bool loop = false;
    public bool playOnAwake = false;

    [Range(0f, 1f)]
    public float volume = 0.5f;

    [Range(0.1f, 3f)]
    public float pitch = 1f;

    [HideInInspector]
    public AudioSource source;

    public void SetSource(AudioSource audioSource)
    {
        source = audioSource;
        source.clip = clip;
        source.loop = loop;
        source.volume = volume;
        source.pitch = pitch;
        source.playOnAwake = playOnAwake;
    }
}
