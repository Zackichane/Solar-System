using UnityEngine;

public class FreeMovement : MonoBehaviour
{
    private Camera currentCamera;
    private Vector3 cameraOriginalPosition;
    private Quaternion cameraOriginalRotation;
    private bool isMainCamera = false;
    private bool freeMovementActive = false;

    void Start()
    {
        freeMovementActive = false;
    }

    void Update()
    {
        // if the camera changes, put the toggle off
        if (currentCamera != Camera.main)
        {
            freeMovementActive = false;
        }
        // if the t is pressed, put the toggle on or off
        if (Input.GetKeyDown(KeyCode.T))
        {
            freeMovementActive = !freeMovementActive;
            OnToggleChanged(freeMovementActive);
        }
    }

    void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
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

    void GetCurrentCamera()
    {
        currentCamera = Camera.main;
        if (currentCamera != null)
        {
            // Store the original position and rotation as values
            cameraOriginalPosition = currentCamera.transform.position;
            cameraOriginalRotation = currentCamera.transform.rotation;
        }
    }
}