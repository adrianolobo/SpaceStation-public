using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    private ParticleSystem engineParticles;

    void Start()
    {
        engineParticles = GetComponent<ParticleSystem>();
        fire();
    }

    public void stop()
    {
        engineParticles.Stop();
    }

    public void fire()
    {
        engineParticles.Play();
    }
}
