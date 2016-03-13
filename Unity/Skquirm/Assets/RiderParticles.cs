using UnityEngine;
using System.Collections;

public class RiderParticles : MonoBehaviour
{

    // particle system variables
    [SerializeField]
    private ParticleSystem left_wave;
    [SerializeField]
    private ParticleSystem right_wave;
    [SerializeField]
    private ParticleSystem bubble;

    public float minEmission;
    public float maxEmission;
    public float bubbleEmission;

    float alpha, beta; //variables used in the linear algebra computation

    public void UpdateParticles(float horizontal, float vertical)
    {
        float emission_right, emission_left, emission_bubbles;

        alpha = (minEmission + maxEmission) / 2f;
        beta = (minEmission - maxEmission) / 2f;

        emission_right = alpha * vertical + beta * horizontal;
        emission_left = alpha * vertical - beta * horizontal;
        emission_bubbles = bubbleEmission * vertical;

        SetEmissionRate(right_wave, emission_right);
        SetEmissionRate(left_wave, emission_left);
        SetEmissionRate(bubble, emission_bubbles);
    }

    public void SetEmissionRate(ParticleSystem particleSystem, float emissionRate)
    {
        var emission = particleSystem.emission;
        var rate = emission.rate;
        rate.constantMax = emissionRate;
        emission.rate = rate;
    }
}
