using System;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
    public class ParticleSystemMultiplier : MonoBehaviour
    {
        // a simple script to scale the size, speed and lifetime of a particle system

        public float multiplier = 1;
        AudioSource source;


        private void Start()
        {
            var systems = GetComponentsInChildren<ParticleSystem>();
            source = GetComponent<AudioSource>();

            foreach (ParticleSystem system in systems)
            {
                system.startSize *= multiplier;
                system.startSpeed *= multiplier;
                system.startLifetime *= Mathf.Lerp(multiplier, 1, 0.5f);
                system.Clear();
                system.Play();
            }
            source.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            source.Play();
        }
    }
}
