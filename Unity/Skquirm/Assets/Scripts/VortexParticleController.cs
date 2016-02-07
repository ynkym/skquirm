using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class VortexParticleController : MonoBehaviour {
  public float rotationSpeed;
  public float pullStrength;
  private ParticleSystem m_System;
  private ParticleSystem.Particle[] m_Particles;

	// Use this for initialization
	void Start () {

	}

  private void LateUpdate()
  {
    InitializeIfNeeded();

    // GetParticles is allocation free because we reuse the m_Particles buffer between updates
    int numParticlesAlive = m_System.GetParticles(m_Particles);

    // Change only the particles that are alive
    for (int i = 0; i < numParticlesAlive; i++)
    {
      Vector3 drift = new Vector3(m_Particles[i].position.z, 0, -1 * m_Particles[i].position.x);
      Vector3 position = new Vector3(m_Particles[i].position.x, 0, m_Particles[i].position.z);
      drift.Normalize();
      position.Normalize();
      m_Particles[i].velocity = rotationSpeed * drift + -pullStrength * position;
    }

    // Apply the particle changes to the particle system
    m_System.SetParticles(m_Particles, numParticlesAlive);
  }

  void InitializeIfNeeded()
  {
    if (m_System == null)
      m_System = GetComponent<ParticleSystem>();

    if (m_Particles == null || m_Particles.Length < m_System.maxParticles)
      m_Particles = new ParticleSystem.Particle[m_System.maxParticles];
  }
}