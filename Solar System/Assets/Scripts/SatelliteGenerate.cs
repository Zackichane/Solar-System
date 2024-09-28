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
    public GameObject S17;
    public GameObject S18;
    public GameObject S19;
    public GameObject S20;
    public GameObject S21;
    public GameObject S22;
    private GameObject generatedPlanet;
    private Transform centerObject;
    public float orbiteSpeed = 100f;
    public float rotationSpeed = 50f;
    private string planetName = "GeneratedPlanet";
    public bool stopOrbite = false;
    public bool randomPrefab = false;

    // add a list of satellites s1 to s22
    public List<GameObject> satellites = new List<GameObject>();

    void Start()
    {
        // Add satellites to the list
        satellites.Add(S1);
        satellites.Add(S2);
        satellites.Add(S3);
        satellites.Add(S4);
        satellites.Add(S5);
        satellites.Add(S6);
        satellites.Add(S7);
        satellites.Add(S8);
        satellites.Add(S9);
        satellites.Add(S10);
        satellites.Add(S11);
        satellites.Add(S12);
        satellites.Add(S13);
        satellites.Add(S14);
        satellites.Add(S15);
        satellites.Add(S16);
        satellites.Add(S17);
        satellites.Add(S18);
        satellites.Add(S19);
        satellites.Add(S20);
        satellites.Add(S21);
        satellites.Add(S22);

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
        if (randomPrefab)
        {
            // Randomly select a satellite prefab
            int index = Random.Range(0, satellites.Count);
            if (satellites[index] != null)
            {
                satellitePrefab = satellites[index];
            }
        }
        else
        {
            // Select a specific satellite prefab
            satellitePrefab = S1;
        }
        if (satellitePrefab == null)
        {
            Debug.LogError("Satellite prefab not found!");
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
        float distance = generatedSatellite.transform.localScale.x + generatedPlanet.transform.localScale.x; // Set the desired distance from the planet
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
