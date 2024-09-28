using UnityEngine;
using UnityEngine.UI;

public class FreeMovement : MonoBehaviour
{
    public Toggle toggle;
    private Camera currentCamera;
    private Vector3 cameraOriginalPosition;
    private Quaternion cameraOriginalRotation;
    private bool isMainCamera = false;

    void Start()
    {
        if (toggle != null)
        {
            toggle.isOn = false;
            toggle.onValueChanged.AddListener(OnToggleChanged);

        }
    }
    void Update()
    {
        // if the camera changes, put the toggle off
        if (currentCamera != Camera.main)
        {
            toggle.isOn = false;
        }
        // if the t is pressed, put the toggle on or off
        if (Input.GetKeyDown(KeyCode.T))
        {
            toggle.isOn = !toggle.isOn;
        }
        
    }
    void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("Toggle is ON");
            GetCurrentCamera();
            if (currentCamera)
            {
                currentCamera.gameObject.AddComponent<CameraController>();
                // get the name of the current camera
                string cameraName = currentCamera.name;
                if (cameraName == "Main Camera")
                {
                    isMainCamera = true;
                }
                else
                {
                    isMainCamera = false;
                    currentCamera.GetComponent<CamObjFollow>().enabled = false;

                }

                
            }
        }
        else
        {
            Debug.Log("Toggle is OFF");
            if (currentCamera)
            {
                Destroy(currentCamera.GetComponent<CameraController>());
                // Reset the original camera's position and rotation
                if (isMainCamera)
                {
                    currentCamera.transform.position = cameraOriginalPosition;
                    currentCamera.transform.rotation = cameraOriginalRotation;
                }
                else
                {
                    currentCamera.GetComponent<CamObjFollow>().enabled = true;
                }
                

            }
        }
    }

    // Optional: Clean up the listener when the script is disabled or destroyed
    void OnDestroy()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(OnToggleChanged);
        }
    }

    void GetCurrentCamera()
    {
        currentCamera = Camera.main;
        if (currentCamera != null)
        {
            Debug.Log("Current camera: " + currentCamera.name);
            // Store the original position and rotation as values
            cameraOriginalPosition = currentCamera.transform.position;
            cameraOriginalRotation = currentCamera.transform.rotation;
            Debug.Log("Original position: " + cameraOriginalPosition);
            Debug.Log("Original rotation: " + cameraOriginalRotation);
        }
        else
        {
            Debug.LogError("Main camera not found!");
        }
    }
}