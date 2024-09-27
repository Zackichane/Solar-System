using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteGenerate : MonoBehaviour
{
    private GameObject satellitePrefab;
    private GameObject generatedSatellite;
    public GameObject S1;
    public GameObject S2;
    public GameObject S3;
    public GameObject S4;
    public GameObject S5;
    public GameObject S6;
    public GameObject S7;
    public GameObject S8;
    public GameObject S9;
    public GameObject S10;
    public GameObject S11;
    public GameObject S12;
    public GameObject S13;
    public GameObject S14;
    public GameObject S15;
    public GameObject S16;
    private GameObject generatedPlanet;
    private Transform centerObject;
    public float orbiteSpeed = 100f;
    public float rotationSpeed = 50f;
    private string planetName = "GeneratedPlanet";
    public bool stopOrbite = false;
    void Start()
    {
        // Find the generated planet in the scene
        generatedPlanet = GameObject.Find(planetName);
        if (generatedPlanet != null)
        {
            centerObject = generatedPlanet.transform;
            GenerateSatellite();
        }
        else
        {
            Debug.LogError("Generated planet not found!");
        }
    }

    void Update()
    {
        // check if the planet was generated
        if (generatedPlanet == null)
        {
            generatedPlanet = GameObject.Find(planetName);
            if (generatedPlanet != null)
            {
                centerObject = generatedPlanet.transform;
                GenerateSatellite();
            }
        }


        GameObject planetObject = GameObject.Find("GeneratedPlanet");
        if (planetObject == null)
        {
            stopOrbite = true;
        }
        else
        {
            stopOrbite = false;
        }

        if (stopOrbite == false)
        {
            OrbiteSatellite();
        }
        RotateSatellite();
    }

    void GenerateSatellite()
    {
        if (S1 == null)
        {
            Debug.LogError("Satellite prefab is not assigned!");
            return;
        }

        // Calculate the size and spawn position of the satellite
        float sizePlanet = generatedPlanet.transform.localScale.x;
        float ratio = 4; // Adjust the ratio as needed
        float sizeSatellite = sizePlanet / ratio;

        // Set the scale of the satellite
        satellitePrefab = S1;
        float distance = sizePlanet * 2f; // Set the desired distance from the planet based on the planet size
        Vector3 spawnPosition = generatedPlanet.transform.position + new Vector3(distance, 0, 0); // Adjust spawn position relative to the planet

        // Instantiate the satellite
        generatedSatellite = Instantiate(satellitePrefab, spawnPosition, Quaternion.identity);
        generatedSatellite.transform.localScale = new Vector3(sizeSatellite, sizeSatellite, sizeSatellite);

        // Rename the satellite
        generatedSatellite.name = "GeneratedSatellite";
    }

    void OrbiteSatellite()
{
    if (generatedSatellite != null && centerObject != null)
    {
        // Ensure the satellite is always at a fixed distance from the planet
        float distance = generatedSatellite.transform.localScale.x; // Set the desired distance from the planet
        Vector3 direction = (generatedSatellite.transform.position - generatedPlanet.transform.position).normalized;
        generatedSatellite.transform.position = generatedPlanet.transform.position + direction * distance;

        // Rotate around the Y axis
        generatedSatellite.transform.RotateAround(centerObject.position, Vector3.up, orbiteSpeed * Time.deltaTime);
    }
}

    void RotateSatellite()
    {
        if (generatedSatellite != null)
        {
            // Implementation for rotating the satellite
            generatedSatellite.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}
