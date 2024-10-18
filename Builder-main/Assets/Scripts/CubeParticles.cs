using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeParticles : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trail;
    [SerializeField] private ParticleSystem _particle;

    //Optimize
    public void PlayCubeLandsParticle()
    {
        _particle.Play();
        _particle.Clear();
    }

    public void TurnOffTrail()
    {
        _trail.enabled = false;
    }

    public void TurnOnTrail(Material mat)
    {
        _trail.enabled = true;
        _trail.sharedMaterial = mat;
    }
}
