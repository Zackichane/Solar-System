using UnityEngine;

public class ButtonTranslation : MonoBehaviour
{
    // Public variables to assign the camera, target position, and rotation in the Inspector
    public Camera mainCamera;  // Reference to the camera to move
    public Vector3 targetPosition;  // Desired position to move to
    public Vector3 targetRotation;  // Desired rotation to rotate to

    // Speed at which the camera should move and rotate (optional)
    public float transitionSpeed = 2f;

    private bool moveCamera = false;  // To trigger the movement
    private bool moveBack = false;    // To trigger moving back

    // Store initial camera position and rotation
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // Thresholds for snapping to final position/rotation to avoid lag
    private float positionThreshold = 0.05f;
    private float rotationThreshold = 2.0f;

    void Start()
    {
        // Save the initial position and rotation of the camera
        initialPosition = mainCamera.transform.position;
        initialRotation = mainCamera.transform.rotation;
    }

    void Update()
    {
        if (moveCamera)
        {
            MoveToPosition(targetPosition, Quaternion.Euler(targetRotation));
        }
        else if (moveBack)
        {
            MoveToPosition(initialPosition, initialRotation);
        }
    }

    // Function to handle the camera movement and snapping to position
    private void MoveToPosition(Vector3 position, Quaternion rotation)
    {
        // Interpolate position and rotation smoothly
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, position, Time.deltaTime * transitionSpeed);
        mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, rotation, Time.deltaTime * transitionSpeed);

        // Check if the camera is within the snapping threshold for position and rotation
        if (Vector3.Distance(mainCamera.transform.position, position) < positionThreshold &&
            Quaternion.Angle(mainCamera.transform.rotation, rotation) < rotationThreshold)
        {
            // Snap to exact position/rotation and stop moving
            mainCamera.transform.position = position;
            mainCamera.transform.rotation = rotation;

            moveCamera = false;
            moveBack = false;
        }
    }

    // This method will be called when the button is clicked to move the camera forward
    public void OnButtonClick()
    {
        moveCamera = true;  // Start moving the camera forward
        moveBack = false;   // Reset move back flag
    }

    // This method will be called when the button is clicked to move the camera back
    public void OnMoveBackClick()
    {
        moveBack = true;  // Start moving the camera back
        moveCamera = false;  // Reset move forward flag
    }

    // This method will be called when the quit button is clicked to quit the game
    public void QuitGame()
    {
        #if UNITY_EDITOR
            Debug.Log("Quit Game option selected.");
        #else
            Application.Quit();  // Quit the game
        #endif
    }
}