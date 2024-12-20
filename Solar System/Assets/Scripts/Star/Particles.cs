using UnityEngine;

public class ParticleFollower : MonoBehaviour
{
    public new ParticleSystem particleSystem;  // Reference to the particle system you want to follow the object

    private Transform particleSystemTransform;

    void Start()
    {
        if (particleSystem != null)
        {
            particleSystemTransform = particleSystem.transform;
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
