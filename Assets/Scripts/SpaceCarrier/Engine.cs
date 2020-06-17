using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    private ParticleSystem engineParticles;
    private AudioSource engineSound;

    void Start()
    {
        engineSound = GetComponent<AudioSource>();
        engineParticles = GetComponent<ParticleSystem>();
    }

    public void stop()
    {
        engineParticles.Stop();
        engineSound.Stop();
    }

    public void fire()
    {
        engineParticles.Play();
        engineSound.Play();
    }
}
