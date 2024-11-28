using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteButtonDeactivation : MonoBehaviour
{
    public GameObject satelliteButton;

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.name != "CAM Planet" && Camera.main.name != "CAM Satellite")
        {
            satelliteButton.SetActive(false);
        }
        else
        {
            satelliteButton.SetActive(true);
        }
    }
}
