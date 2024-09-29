using UnityEngine;

public class CameraChangerButton : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondaryCamera;

    void Start()
    {
        // Ensure only the main camera is active at the start
        mainCamera.enabled = true;
        secondaryCamera.enabled = false;
    }

    // This method is called by the button to switch cameras
    public void SwitchCamera()
    {
        // Toggle the cameras
        if (mainCamera.enabled)
        {
            mainCamera.enabled = false;
            secondaryCamera.enabled = true;
        }
        else
        {
            mainCamera.enabled = true;
            secondaryCamera.enabled = false;
        }
    }
} 
