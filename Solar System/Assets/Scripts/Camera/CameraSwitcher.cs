using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // List of all cameras
    public Canvas[] canvas; // List of all canvases
    public GameObject[] objectsToToggle; // List of objects whose mesh renderers we want to toggle
    private int currentCameraIndex = 0; // Index of the currently active camera

    void Start()
    {
        // Initially disable all cameras and canvases except the first one
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = false;
            canvas[i].enabled = false;

            // Disable the mesh renderer for all objects initially
            if (objectsToToggle.Length > i)
            {
                MeshRenderer meshRenderer = objectsToToggle[i].GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = false;
                }
            }
        }

        if (cameras.Length > 0)
        {
            cameras[currentCameraIndex].enabled = true; // Enable the first camera
            canvas[currentCameraIndex].enabled = true; // Enable the first canvas

            // Enable the mesh renderer of the object related to the first camera
            if (objectsToToggle.Length > currentCameraIndex)
            {
                MeshRenderer meshRenderer = objectsToToggle[currentCameraIndex].GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = true;
                }
            }
        }
    }

    // Switch to the next camera in the list
    public void SwitchToNextCamera()
    {
        // Disable the current camera and canvas
        cameras[currentCameraIndex].enabled = false;
        canvas[currentCameraIndex].enabled = false;

        // Disable the mesh renderer of the current object
        if (objectsToToggle.Length > currentCameraIndex)
        {
            MeshRenderer meshRenderer = objectsToToggle[currentCameraIndex].GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }
        }

        // Update the camera index, cycling back to the first camera if necessary
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // Enable the next camera and canvas
        cameras[currentCameraIndex].enabled = true;
        canvas[currentCameraIndex].enabled = true;

        // Enable the mesh renderer of the new object
        if (objectsToToggle.Length > currentCameraIndex)
        {
            MeshRenderer meshRenderer = objectsToToggle[currentCameraIndex].GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }
        }
    }

    // Switch to the previous camera in the list
    public void SwitchToPreviousCamera()
    {
        // Disable the current camera and canvas
        cameras[currentCameraIndex].enabled = false;
        canvas[currentCameraIndex].enabled = false;

        // Disable the mesh renderer of the current object
        if (objectsToToggle.Length > currentCameraIndex)
        {
            MeshRenderer meshRenderer = objectsToToggle[currentCameraIndex].GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }
        }

        // Update the camera index, cycling back to the last camera if necessary
        currentCameraIndex = (currentCameraIndex - 1 + cameras.Length) % cameras.Length;

        // Enable the previous camera and canvas
        cameras[currentCameraIndex].enabled = true;
        canvas[currentCameraIndex].enabled = true;

        // Enable the mesh renderer of the new object
        if (objectsToToggle.Length > currentCameraIndex)
        {
            MeshRenderer meshRenderer = objectsToToggle[currentCameraIndex].GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }
        }
    }
}
