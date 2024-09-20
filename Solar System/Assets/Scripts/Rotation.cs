using UnityEngine;

public class RotateSphere : MonoBehaviour
{
    // Speed of rotation
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotate the sphere around its Y-axis (can be changed to X or Z)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}