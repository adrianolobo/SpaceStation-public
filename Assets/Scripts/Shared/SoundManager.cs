using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public List<Sound> sounds;
    void Awake()
    {
        // Não funfa pra multiplos sons ao mesmo tempo T_T
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

}
