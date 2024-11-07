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
    private string planetName = "GeneratedPlanet";
    public bool stopOrbite = false;
    public bool randomPrefab = false;
    public float orbitBuffer = 100f; // Base buffer distance to avoid collisions
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
                    for (int j = 0; j < numSatellites; j++)
                    {
                        GenerateSatellite(centerObject.gameObject);
                    }
                }
            }
        }


        //GameObject planetObject = GameObject.Find("GeneratedPlanet");
        //if (planetObject == null)
        //{
        //    stopOrbite = true;
        //}
        //else
        //{
        //    stopOrbite = false;
        //}
//
        //if (stopOrbite == false)
        //{
        //    OrbiteSatellite();
        //}
        //RotateSatellite();
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

        // Calculate the size and spawn position of the satellite
        float sizePlanet = planetObject.transform.localScale.x;
        float minSize = 10000/scale;
        float maxSize = 15868/scale;
        float sizeSatellite = Random.Range(minSize, maxSize);

        // Set the scale of the satellite
        float distance = sizePlanet * 2f; // Set the desired distance from the planet based on the planet size
        float angle = satelliteCounter * 30f; // Calculate angle for unique position
        Vector3 spawnPosition = planetObject.transform.position + new Vector3(distance * Mathf.Cos(angle), 0, distance * Mathf.Sin(angle)); // Adjust spawn position relative to the planet

        // Instantiate the satellite
        generatedSatellite = Instantiate(satellitePrefab, spawnPosition, Quaternion.identity);
        generatedSatellite.transform.localScale = new Vector3(sizeSatellite, sizeSatellite, sizeSatellite);

        // Rename the satellite with a unique identifier
        generatedSatellite.name = "GeneratedSatellite" + planetObject.name.Substring(15) + "_" + satelliteCounter++;
        // add a tag
        generatedSatellite.tag = "GeneratedSatellite";
        generatedSatellite.AddComponent<SatelliteRotationManager>();

        // generate a camera for the satellite
        GameObject camera = new GameObject();
        camera.AddComponent<Camera>();
        camera.AddComponent<CamObjFollow>();
        camera.GetComponent<CamObjFollow>().targetName = generatedSatellite.name;
        camera.GetComponent<CamObjFollow>().secondTargetName = planetObject.name;
        camera.name = "Camera" + generatedSatellite.name;
        camera.tag = "MainCamera";
        camera.GetComponent<Camera>().enabled = false;
    }

    void ArrangeSatellitesInOrbits(GameObject planetObject, List<GameObject> satellites)
    {
        float currentOrbitDistance = planetObject.transform.localScale.x * 2; // Start orbit distance based on planet size
        float largestSatelliteSize = 0f;

        // Find the largest satellite size
        foreach (GameObject satellite in satellites)
        {
            float satelliteSize = satellite.transform.localScale.x;
            if (satelliteSize > largestSatelliteSize)
            {
                largestSatelliteSize = satelliteSize;
            }
        }

        for (int i = 0; i < satellites.Count; i++)
        {
            GameObject satellite = satellites[i];
            float satelliteSize = satellite.transform.localScale.x;

            // Calculate orbit distance based on the largest satellite size with buffer
            currentOrbitDistance += largestSatelliteSize * 2 + orbitBuffer;

            // Set the satellite's position on its orbit
            satellite.transform.position = planetObject.transform.position + new Vector3(currentOrbitDistance, 0, 0);


            // Increase orbit distance for the next satellite
            currentOrbitDistance += largestSatelliteSize * 2 + orbitBuffer;
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
