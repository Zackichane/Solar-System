using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatelliteButtonDeactivation : MonoBehaviour
{
    public Button satelliteButton;

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.name != "CAM Planet" && Camera.main.name != "CAM Satellite")
        {
            satelliteButton.interactable = false;
        }
        else
        {
            satelliteButton.interactable = true;
        }
    }
}
