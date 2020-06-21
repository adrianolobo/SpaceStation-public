using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public List<Sound> sounds;
    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.setAudioSource(gameObject.AddComponent<AudioSource>());
        }
    }

    public void Play(Sounds.SOUND sound)
    {
        Sound soundToPlay = getSound(sound);
        if (soundToPlay == null) return;
        soundToPlay.Play();
    }

    public void Stop(Sounds.SOUND sound)
    {
        Sound soundToStop = getSound(sound);
        if (soundToStop == null) return;
        soundToStop.Stop();
    }


    public Sound getSound(Sounds.SOUND sound)
    {
        return sounds.Find(soundItem => soundItem.name == sound);
    }

    public Sound createSound(Sounds.SOUND sound)
    {
        Sound soundToClone = sounds.Find(soundItem => soundItem.name == sound);
        Sound newSound = new Sound();
        newSound.volume = soundToClone.volume;
        newSound.pitch = soundToClone.pitch;
        newSound.name = soundToClone.name;
        newSound.loop = soundToClone.loop;
        newSound.audioClip = soundToClone.audioClip;
        newSound.playOnAwake = soundToClone.playOnAwake;
        newSound.setAudioSource(gameObject.AddComponent<AudioSource>());
        return newSound;
    }
}
