using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // List of all cameras
    private int currentCameraIndex = 0; // Index of the currently active camera

    void Start()
    {
        // Initially disable all cameras except the first one
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = false;
        }

        if (cameras.Length > 0)
        {
            cameras[currentCameraIndex].enabled = true; // Enable the first camera
        }
    }

    // Switch to the next camera in the list
    public void SwitchToNextCamera()
    {
        // Disable the current camera
        cameras[currentCameraIndex].enabled = false;

        // Update the camera index, cycling back to the first camera if necessary
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // Enable the next camera
        cameras[currentCameraIndex].enabled = true;
    }

    // Switch to the previous camera in the list
    public void SwitchToPreviousCamera()
    {
        // Disable the current camera
        cameras[currentCameraIndex].enabled = false;

        // Update the camera index, cycling back to the last camera if necessary
        currentCameraIndex = (currentCameraIndex - 1 + cameras.Length) % cameras.Length;

        // Enable the previous camera
        cameras[currentCameraIndex].enabled = true;
    }
}
