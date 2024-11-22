using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteGenerate : MonoBehaviour
{
    private GameObject satellitePrefab;
    private const float scale = 10000f;
    private GameObject generatedSatellite;
    private GameObject[] generatedPlanet;
    private Transform centerObject;
    public float orbiteSpeed = 100f;
    public float rotationSpeed = 50f;
    public bool stopOrbite = false;
    public bool randomPrefab = false;
    public float orbitBuffer = 10f; // Increased buffer distance to avoid collisions
    private int satelliteCounter = 0; // Add a counter for unique satellite names

    // add a list of satellites s1 to s22
    public List<GameObject> satellites = new List<GameObject>();

    void Start()
    {
        // Shuffle the list of satellites to ensure randomness
        ShuffleSatellites();
    }

    void Update()
    {
        // check if the planet was generated
        if (generatedPlanet == null)
        {
            generatedPlanet = GameObject.FindGameObjectsWithTag("GeneratedPlanet");

            if (generatedPlanet != null && generatedPlanet.Length > 0)
            {
                // generate a satellite for each planet
                for (int i = 0; i < generatedPlanet.Length; i++)
                {
                    centerObject = generatedPlanet[i].transform;
                    int minSatellites = 1;
                    int maxSatellites = 5;
                    int numSatellites = Random.Range(minSatellites, maxSatellites + 1);
                    List<GameObject> planetSatellites = new List<GameObject>();
                    for (int j = 0; j < numSatellites; j++)
                    {
                        GenerateSatellite(centerObject.gameObject);
                        planetSatellites.Add(generatedSatellite);
                    }
                    ArrangeSatellitesInOrbits(centerObject.gameObject, planetSatellites);
                }
            }
        }
    }

    void GenerateSatellite(GameObject planetObject)
    {
        if (randomPrefab)
        {
            // Randomly select a satellite prefab and remove it from the list
            if (satellites.Count > 0)
            {
                int index = Random.Range(0, satellites.Count);
                satellitePrefab = satellites[index];
                satellites.RemoveAt(index);
            }
            else
            {
                // No prefabs left to generate satellites
                return;
            }
        }
        if (satellitePrefab == null)
        {
            return;
        }

        // Determine satellite size range based on planet type(
        float minSize, maxSize;
        string planetTypeValue = planetObject.GetComponent<planetType>().planet_type;

        switch (planetTypeValue)
        {
            case "MercuryPlanets":
                minSize = 1000 / scale;
                maxSize = 3000 / scale;
                break;
            case "VenusPlanets":
                minSize = 3000 / scale;
                maxSize = 8000 / scale;
                break;
            case "MarsPlanets":
                minSize = 2000 / scale;
                maxSize = 4000 / scale;
                break;
            case "RockyPlanets":
                minSize = 5000 / scale;
                maxSize = 10000 / scale;
                break;
            case "GasPlanets":
                minSize = 5000 / scale;
                maxSize = 20000 / scale;
                break;
            default:
                minSize = 1000 / scale;
                maxSize = 5000 / scale;
                break;
        }

        // Calculate the size and spawn position of the satellite
        float sizePlanet = planetObject.transform.localScale.x;
        float sizeSatellite = Random.Range(minSize, maxSize);

        // Set the spawn position of the satellite
        float distance = sizePlanet * 2f; // Set the desired distance from the planet based on the planet size
        float angle = satelliteCounter * 30f; // Calculate angle for unique position
        Vector3 spawnPosition = planetObject.transform.position + new Vector3(distance * Mathf.Cos(angle), 0, distance * Mathf.Sin(angle)); // Adjust spawn position relative to the planet

        // Instantiate the satellite
        generatedSatellite = Instantiate(satellitePrefab, spawnPosition, Quaternion.identity);
        generatedSatellite.transform.localScale = new Vector3(sizeSatellite, sizeSatellite, sizeSatellite);

        // Rename the satellite with a unique identifier
        generatedSatellite.name = "GeneratedSatellite" + planetObject.name.Substring(15) + "_" + satelliteCounter++;
        generatedSatellite.tag = "GeneratedSatellite";
        

        // Attach planet reference
        generatedSatellite.AddComponent<planetTracker>();
        generatedSatellite.GetComponent<planetTracker>().planet = planetObject;
    }

    void ArrangeSatellitesInOrbits(GameObject planetObject, List<GameObject> satellites)
    {
        Debug.Log("Arranging satellites in orbits...");
        float planetRadius = planetObject.transform.localScale.x; // Get the planet's radius and double it to get the minimum distance from the planet
        float largestSatelliteSize = 0f;

        // Find the largest satellite size to calculate the spacing
        foreach (GameObject satellite in satellites)
        {
            float satelliteSize = satellite.transform.localScale.x;
            if (satelliteSize > largestSatelliteSize)
            {
                largestSatelliteSize = satelliteSize;
            }
        }

        // Buffer distance between each orbit to avoid collision (increased spacing for larger satellites)
        float orbitSpacing = largestSatelliteSize + orbitBuffer;

        // Arrange each satellite with a unique orbit distance
        for (int i = 0; i < satellites.Count; i++)
        {
            GameObject satellite = satellites[i];
            float satelliteSize = satellite.transform.localScale.x;

            // Calculate the unique orbit distance for each satellite, progressively spaced
            float satelliteOrbitDistance = planetRadius + (i * orbitSpacing);  // Progressively increase orbit distance

            // Add a random buffer to the x-coordinate
            float randomBuffer = 0f;//Random.Range(20f, 70f);
            Debug.Log("adding a buffer of " + randomBuffer + " to the x-coordinate");

            satellite.transform.position = new Vector3(satelliteOrbitDistance + randomBuffer, 0, 0) + planetObject.transform.position;
            Debug.Log("Satellite " + satellite.name + " is at position " + satellite.transform.position);

            satellite.AddComponent<SatelliteRotationManager>();
        }
    }

    void ShuffleSatellites()
    {
        for (int i = 0; i < satellites.Count; i++)
        {
            GameObject temp = satellites[i];
            int randomIndex = Random.Range(i, satellites.Count);
            satellites[i] = satellites[randomIndex];
            satellites[randomIndex] = temp;
        }
    }
}
