using UnityEngine;

public class ParticleFollower : MonoBehaviour
{
    public ParticleSystem particleSystem;  // Reference to the particle system you want to follow the object

    private Transform particleSystemTransform;

    void Start()
    {
        if (particleSystem != null)
        {
            particleSystemTransform = particleSystem.transform;
        }
        else
        {
            Debug.LogError("Particle system is not assigned!");
        }
    }

    void Update()
    {
        if (particleSystem != null)
        {
            // Update the particle system's position to follow this object's position
            particleSystemTransform.position = transform.position;
        }
    }
}
