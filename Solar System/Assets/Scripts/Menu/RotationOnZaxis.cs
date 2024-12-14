using UnityEngine;

public class RotationOnZAxis : MonoBehaviour
{
    // Speed of rotation
    public float rotationSpeed = 20f;

    void Update()
    {
        // Rotate the object around its Z-axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}