using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // List of all cameras
    public Canvas[] canvas; // List of all canvases
    public GameObject[] objectsToToggle; // List of objects whose mesh renderers we want to toggle
    private int currentCameraIndex = 0; // Index of the currently active camera
    public Camera mainCamera;

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
                DisableMeshRenderers(objectsToToggle[i]);
            }
        }

        if (cameras.Length > 0 && mainCamera.enabled == false)
        {
            cameras[currentCameraIndex].enabled = true; // Enable the first camera
            canvas[currentCameraIndex].enabled = true; // Enable the first canvas

            // Enable the mesh renderer of the object related to the first camera
            if (objectsToToggle.Length > currentCameraIndex)
            {
                EnableMeshRenderers(objectsToToggle[currentCameraIndex]);
            }
        }
    }

    // Switch to the next camera in the list
    public void SwitchToNextCamera()
    {
        // Disable the current camera and canvas
        cameras[currentCameraIndex].enabled = false;
        canvas[currentCameraIndex].enabled = false;

        // Disable the mesh renderer of the current object and its children
        if (objectsToToggle.Length > currentCameraIndex)
        {
            DisableMeshRenderers(objectsToToggle[currentCameraIndex]);
        }

        // Update the camera index, cycling back to the first camera if necessary
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // Enable the next camera and canvas
        cameras[currentCameraIndex].enabled = true;
        canvas[currentCameraIndex].enabled = true;

        // Enable the mesh renderer of the new object and its children
        if (objectsToToggle.Length > currentCameraIndex)
        {
            EnableMeshRenderers(objectsToToggle[currentCameraIndex]);
        }
    }

    // Switch to the previous camera in the list
    public void SwitchToPreviousCamera()
    {
        // Disable the current camera and canvas
        cameras[currentCameraIndex].enabled = false;
        canvas[currentCameraIndex].enabled = false;

        // Disable the mesh renderer of the current object and its children
        if (objectsToToggle.Length > currentCameraIndex)
        {
            DisableMeshRenderers(objectsToToggle[currentCameraIndex]);
        }

        // Update the camera index, cycling back to the last camera if necessary
        currentCameraIndex = (currentCameraIndex - 1 + cameras.Length) % cameras.Length;

        // Enable the previous camera and canvas
        cameras[currentCameraIndex].enabled = true;
        canvas[currentCameraIndex].enabled = true;

        // Enable the mesh renderer of the new object and its children
        if (objectsToToggle.Length > currentCameraIndex)
        {
            EnableMeshRenderers(objectsToToggle[currentCameraIndex]);
        }
    }

    // Function to disable mesh renderers for an object and all its children
    private void DisableMeshRenderers(GameObject obj)
    {
        MeshRenderer[] renderers = obj.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    // Function to enable mesh renderers for an object and all its children
    private void EnableMeshRenderers(GameObject obj)
    {
        MeshRenderer[] renderers = obj.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = true;
        }
    }
}
