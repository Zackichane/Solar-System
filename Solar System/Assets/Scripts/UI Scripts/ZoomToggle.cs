using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomToggle : MonoBehaviour
{
    public Camera[] cameras;
    public Toggle toggle;
    private Camera activeCamera;
    private Camera oldCamera;
    // Start is called before the first frame update
    void Start()
    {
        toggle.isOn = false;
        toggle.onValueChanged.AddListener(ToggleValueChanged);
        GetActiveCamera();
    }

    void Update()
    {
        if (activeCamera.enabled == false)
        {
            ToggleValueChanged(false);
        }
    }

    void ToggleValueChanged(bool value)
    {
        GetActiveCamera();
        if (oldCamera != null && oldCamera != activeCamera)
        {
            Destroy(oldCamera.GetComponent<CameraZoom>());
            EnableOtherScripts(oldCamera);
        }
        if (value)
        {
            if (activeCamera.GetComponent<CameraZoom>() == null)
            {
                activeCamera.gameObject.AddComponent<CameraZoom>();
            }
            activeCamera.GetComponent<CameraZoom>().enabled = true;
            DisableOtherScripts(activeCamera);
        }
        else
        {
            if (activeCamera.GetComponent<CameraZoom>() != null)
            {
                activeCamera.GetComponent<CameraZoom>().enabled = false;
            }
            EnableOtherScripts(activeCamera);
        }
        toggle.isOn = value;
    }

    void GetActiveCamera()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].enabled)
            {
                oldCamera = activeCamera;
                activeCamera = cameras[i];
                break;
            }
        }
    }

    void DisableOtherScripts(Camera camera)
    {
        Debug.Log(camera.name);
        if (camera.name == "Main Camera")
        {
            camera.GetComponent<AdjustMainCamera>().enabled = false;
        }
        else
        {
            camera.GetComponent<CamObjFollow>().enabled = false;
        }
    }

    void EnableOtherScripts(Camera camera)
    {
        Debug.Log(camera.name);
        if (camera.name == "Main Camera")
        {
            camera.GetComponent<AdjustMainCamera>().enabled = true;
        }
        else
        {
            camera.GetComponent<CamObjFollow>().enabled = true;
        }
    }
}
