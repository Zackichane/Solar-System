using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Public variables to assign the camera, target position, and rotation in the Inspector
    public Camera mainCamera;  // Reference to the camera to move
    public Vector3 targetPosition;  // Desired position to move to
    public Vector3 targetRotation;  // Desired rotation to rotate to

    // Speed at which the camera should move and rotate (optional)
    public float transitionSpeed = 2f;

    private bool moveCamera = false;  // To trigger the movement

    void Update()
    {
        if (moveCamera)
        {
            // Interpolate position and rotation smoothly
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * transitionSpeed);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * transitionSpeed);

            // Check if the camera has reached the target position and rotation
            if (Vector3.Distance(mainCamera.transform.position, targetPosition) < 0.01f &&
                Quaternion.Angle(mainCamera.transform.rotation, Quaternion.Euler(targetRotation)) < 0.01f)
            {
                moveCamera = false;  // Stop moving once close enough
            }
        }
    }

    // This method will be called when the button is clicked to move the camera
    public void OnButtonClick()
    {
        moveCamera = true;  // Start moving the camera
    }

    // This method will be called when the quit button is clicked to quit the game
    public void QuitGame()
    {
        // Log a message in the Unity Editor, but quit the application in a built game
        #if UNITY_EDITOR
            Debug.Log("Quit Game option selected.");
        #else
            Application.Quit();  // Quit the game
        #endif
    }
}
