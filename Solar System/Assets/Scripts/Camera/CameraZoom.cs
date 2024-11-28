using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private float zoomSpeed = 1000f;  // Adjustable zoom speed per camera
    private float rotateSpeed = 5f; // Speed at which the camera rotates based on mouse movement

    private new Camera camera;
    private Vector3 lastMousePosition;
    private float currentXRotation = 0f;
    private float currentYRotation = 0f;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        HandleZoom();
        HandleRotation();
    }

    void HandleZoom()
    {
        // Get the scroll wheel input from the user (positive scroll = zoom in, negative scroll = zoom out)
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Zoom the camera based on the scroll wheel input
        if (scrollInput != 0f)
        {
            // Adjust the camera's position based on the scroll input
            Vector3 move = new Vector3(-scrollInput * zoomSpeed, -scrollInput * zoomSpeed, 0);
            transform.Translate(move, Space.World);
        }
    }

    void HandleRotation()
    {
        // Check if the right mouse button is held down
        if (Input.GetMouseButtonDown(1)) // Right mouse button down
        {
            lastMousePosition = Input.mousePosition; // Store initial mouse position
        }

        if (Input.GetMouseButton(1)) // Right mouse button held down
        {
            // Get the mouse movement delta (how much the mouse has moved)
            Vector3 delta = Input.mousePosition - lastMousePosition;

            // Calculate the new rotation based on the mouse movement
            currentXRotation -= delta.y * rotateSpeed * Time.deltaTime; // For up/down movement (X-axis)
            currentYRotation += delta.x * rotateSpeed * Time.deltaTime; // For left/right movement (Y-axis)

            // Optional: Clamp the X rotation to avoid flipping (e.g., you can restrict it to -90 to 90 degrees)
            currentXRotation = Mathf.Clamp(currentXRotation, -90f, 90f);

            // Apply the rotation to the camera
            transform.localRotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);

            // Update the last mouse position for continuous rotation
            lastMousePosition = Input.mousePosition;
        }
    }
}
