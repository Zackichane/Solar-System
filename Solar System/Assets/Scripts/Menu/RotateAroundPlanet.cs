using UnityEngine;

public class RotateAroundPlanet : MonoBehaviour
{
    public Transform target; // The object to rotate around
    public float rotationSpeed = 20f; // Speed of rotation
    public Vector3 rotationAxis = Vector3.up; // Axis around which to rotate

    void Update()
    {
        if (target != null)
        {
            // Rotate around the target
            transform.RotateAround(target.position, rotationAxis, rotationSpeed * Time.deltaTime);
        }
    }
}
