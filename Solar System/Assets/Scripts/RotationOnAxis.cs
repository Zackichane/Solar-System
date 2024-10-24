using UnityEngine;

public class RotationOnAxis : MonoBehaviour
{
    // Speed of rotation
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotate the sphere around its Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
