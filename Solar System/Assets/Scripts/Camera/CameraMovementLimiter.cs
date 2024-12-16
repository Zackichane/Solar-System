using UnityEngine;

public class CameraMovementLimiter : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float leftLimit = -10f;
    public float rightLimit = 10f;
    private float initialX;

    void Start()
    {
        initialX = transform.position.x;
    }

    void Update()
    {
        // Get horizontal input from arrow keys (or A/D keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the new position based on input and speed
        float newX = transform.position.x + horizontalInput * moveSpeed * Time.deltaTime;

        // Clamp the X position to stay within the boundaries
        newX = Mathf.Clamp(newX, leftLimit + initialX, rightLimit + initialX);

        // Update the camera's position
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
