using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScriptDeactivation : MonoBehaviour
{
    public Camera[] cameras;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = false;
            cameras[i].GetComponent<CameraMovementLimiter>().enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].enabled == true)
            {
                cameras[i].GetComponent<CameraMovementLimiter>().enabled = true;
            }
            else
            {
                cameras[i].GetComponent<CameraMovementLimiter>().enabled = false;
                cameras[i].transform.position = cameras[i].GetComponent<CameraMovementLimiter>().initialPosition;
            }
        }
    }
}
