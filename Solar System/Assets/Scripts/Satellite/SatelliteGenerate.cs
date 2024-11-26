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
    public float orbitBuffer = 0f; // Increased buffer distance to avoid collisions
    private int satelliteCounter = 0;

    public List<GameObject> satellites = new List<GameObject>();
    public GameObject blueSpherePrefab; // Object to match position and scale
    private Dictionary<GameObject, GameObject> satelliteObjectMap = new Dictionary<GameObject, GameObject>();

    void Start()
    {
        ShuffleSatellites();
    }

    void Update()
    {
        if (generatedPlanet == null)
        {
            generatedPlanet = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
            if (generatedPlanet.Length == 0) generatedPlanet = null;

            if (generatedPlanet != null && generatedPlanet.Length > 0)
            {
                for (int i = 0; i < generatedPlanet.Length; i++)
                {
                    centerObject = generatedPlanet[i].transform;
                    int minSatellites = 0;
                    int maxSatellites = 3;
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
            if (satellites.Count > 0)
            {
                int index = Random.Range(0, satellites.Count);
                satellitePrefab = satellites[index];
                satellites.RemoveAt(index);
            }
            else
            {
                return;
            }
        }

        if (satellitePrefab == null) return;

        float minSize, maxSize;
        string planetTypeValue = planetObject.GetComponent<planetInfos>().planetType;

        switch (planetTypeValue)
        {
            case "MercuryPlanets":
                minSize = 4000 / scale;
                maxSize = 7000 / scale;
                break;
            case "VenusPlanets":
                minSize = 6000 / scale;
                maxSize = 12000 / scale;
                break;
            case "MarsPlanets":
                minSize = 4000 / scale;
                maxSize = 9000 / scale;
                break;
            case "RockyPlanets":
                minSize = 7000 / scale;
                maxSize = 12000 / scale;
                break;
            case "GasPlanets":
                minSize = 8000 / scale;
                maxSize = 20000 / scale;
                break;
            default:
                minSize = 1000 / scale;
                maxSize = 5000 / scale;
                break;
        }

        float sizePlanet = planetObject.transform.localScale.x;
        float sizeSatellite = Random.Range(minSize, maxSize);

        float distance = sizePlanet * 2f;
        float angle = satelliteCounter * 30f;
        Vector3 spawnPosition = planetObject.transform.position + new Vector3(distance * Mathf.Cos(angle), 0, distance * Mathf.Sin(angle));

        generatedSatellite = Instantiate(satellitePrefab, spawnPosition, Quaternion.identity);
        generatedSatellite.transform.localScale = new Vector3(sizeSatellite, sizeSatellite, sizeSatellite);

        generatedSatellite.name = "GeneratedSatellite" + planetObject.name.Substring(15) + "_" + satelliteCounter++;
        generatedSatellite.tag = "GeneratedSatellite";

        generatedSatellite.AddComponent<planetTracker>();
        generatedSatellite.GetComponent<planetTracker>().planet = planetObject;

        // Set the satellite as a child of the planet object
        generatedSatellite.transform.parent = planetObject.transform;

        // create the blue sphere
        InstantiateBlueSpheres(generatedSatellite);
    }

    void ArrangeSatellitesInOrbits(GameObject planetObject, List<GameObject> satellites)
    {
        float planetRadius = planetObject.transform.localScale.x;
        float largestSatelliteSize = 0f;

        foreach (GameObject satellite in satellites)
        {
            float satelliteSize = satellite.transform.localScale.x;
            if (satelliteSize > largestSatelliteSize)
            {
                largestSatelliteSize = satelliteSize;
            }
        }

        float orbitSpacing = largestSatelliteSize + orbitBuffer;

        for (int i = 0; i < satellites.Count; i++)
        {
            GameObject satellite = satellites[i];
            float satelliteOrbitDistance = planetRadius/2 + orbitSpacing/2;
            float randomBuffer = 0f;
            satellite.transform.position = new Vector3(satelliteOrbitDistance, 0, 0) + planetObject.transform.position;

            satellite.AddComponent<SatelliteRotationManager>();
        }
    }

    void InstantiateBlueSpheres(GameObject satellite)
    {
        GameObject blueSphere = Instantiate(blueSpherePrefab, satellite.transform.position, Quaternion.identity);
        blueSphere.transform.localScale = new Vector3(60, 60, 60);
        blueSphere.transform.parent = satellite.transform;
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
