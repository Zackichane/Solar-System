using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1;  // First camera
    public Camera camera2;  // Second camera
    private bool isCamera1Active = true;  // Track which camera is active

    void Start()
    {
        // Ensure only the first camera is active at the start
        camera1.enabled = true;
        camera2.enabled = false;
    }

    void Update()
    {
        // Switch cameras when R is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            isCamera1Active = !isCamera1Active;  // Toggle active camera

            // Enable the active camera, disable the other one
            camera1.enabled = isCamera1Active;
            camera2.enabled = !isCamera1Active;

            // Toggle movement control based on which camera is active
            camera1.GetComponent<CameraController>().enabled = isCamera1Active;
            camera2.GetComponent<CameraController>().enabled = !isCamera1Active;
        }
    }
}
