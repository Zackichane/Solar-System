using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteRotationManager : MonoBehaviour
{
    // set the centerobject position to 0,0,0
    private Transform centerObject;
    private float rotationSpeed = 1f;
    private bool stopOrbite = false;
    private string name;
    private string number;
    private GameObject generatedPlanet;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(1f, 20f);
        centerObject = GetComponent<planetTracker>().planet.transform;
        generatedPlanet = centerObject.gameObject; // Ensure generatedPlanet is assigned
    }

    // Update is called once per frame
    void Update()
    {
        OrbiteSatellite();
        RotateSatellite();
    }

    void OrbiteSatellite()
    {
        if (centerObject != null && generatedPlanet != null)
        {
            // Ensure the satellite is always at a fixed distance from the planet
            float distance = transform.localScale.x + generatedPlanet.transform.localScale.x; // Set the desired distance from the planet
            Vector3 direction = (transform.position - generatedPlanet.transform.position).normalized;
            transform.position = generatedPlanet.transform.position + direction * distance;

            // Rotate around the Y axis
            transform.RotateAround(centerObject.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    void RotateSatellite()
    {
        // Implementation for rotating the satellite
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
