using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleToPlayer : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 5f;

    private ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;

    private bool isActive;
    private AudioSource audioSource;
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        audioSource = GetComponent<AudioSource>();
        Invoke("Active", 1.5f);

    }

    void Update()
    {
        if (!isActive)
            return;
        int numParticlesAlive = particleSystem.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 direction = (target.position - particles[i].position).normalized;
            particles[i].position = Vector3.MoveTowards(particles[i].position, direction, speed * Time.deltaTime);

        }

        particleSystem.SetParticles(particles, numParticlesAlive);

    }

    void Active()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
        isActive = true;
    }

    public void OnParticleSystemStopped()
    {
        isActive = false;
    }
}
