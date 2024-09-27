using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCAMplanet_satellite : MonoBehaviour
{
    private GameObject planet;
    private GameObject satellite;
    private Vector3 planetPosition;
    private Vector3 satellitePosition;
    private bool cameraAdjusted = false;
    void Start()
    {
        // try to get the planet and star position
        GetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (planet == null || satellite == null)
        {
            // if the planet or star is not found, try to find them again
            GetPosition();
        }
        if (planet != null && satellite != null && !cameraAdjusted)
        {
            planetPosition = planet.transform.position; // Ensure planetPosition is updated to the current position of the planet
            float planetX = planetPosition.x;
            float satelliteX = satellite.transform.position.x;
            float distPlanetSatellite = Mathf.Abs(satelliteX - planetX); // Use Mathf.Abs to get the absolute distance
            // use tan to calculate the distance between the planet and the satellite
            float dist = distPlanetSatellite / 0.5773502692f;
            dist += satellite.transform.localScale.x / 2;
            // set the object position
            transform.position = new Vector3(planetX, 0, dist);
            cameraAdjusted = true;
        }

    }

    void GetPosition()
    {
        planet = GameObject.Find("GeneratedPlanet");
        satellite = GameObject.Find("GeneratedSatellite");
        if (planet != null)
        {
            planetPosition = planet.transform.position;
        }
        if (satellite != null)
        {
            satellitePosition = satellite.transform.position;
        }
    }
}
