using UnityEngine;
using System.Collections;

[System.Serializable]
public class Sound
{
    public Sounds.SOUND name;
    public bool loop = false;
    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(.1f, 3f)]
    public float pitch = 1f;

    public bool playOnAwake = false;

    private AudioSource audioSource;
    public void setAudioSource(AudioSource audioSource)
    {
        this.audioSource = audioSource;
        this.audioSource.volume = volume;
        this.audioSource.pitch = pitch;
        this.audioSource.playOnAwake = playOnAwake;
        this.audioSource.clip = audioClip;
        this.audioSource.loop = loop;
    }

    public void Play()
    {
        audioSource.Play();
    }
    public void Stop()
    {
        audioSource.Stop();
    }
}
