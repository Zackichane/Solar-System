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
    private int satelliteCounter = 0;

    public List<GameObject> satellites = new List<GameObject>();
    public GameObject inspectorAssignedObject; // Object to match position and scale
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

            if (generatedPlanet != null && generatedPlanet.Length > 0)
            {
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

                        // Create and manage inspectorAssignedObject
                        if (inspectorAssignedObject != null)
                        {
                            GameObject newObject = Instantiate(inspectorAssignedObject);
                            newObject.transform.localScale = generatedSatellite.transform.localScale * 6f;
                            satelliteObjectMap[generatedSatellite] = newObject; // Map the satellite to its assigned object
                        }
                    }
                    ArrangeSatellitesInOrbits(centerObject.gameObject, planetSatellites);
                }
            }
        }

        UpdateInspectorAssignedObjects();
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
            float satelliteOrbitDistance = planetRadius + (i * orbitSpacing);
            float randomBuffer = 0f;
            satellite.transform.position = new Vector3(satelliteOrbitDistance + randomBuffer, 0, 0) + planetObject.transform.position;

            satellite.AddComponent<SatelliteRotationManager>();
        }
    }

    void UpdateInspectorAssignedObjects()
    {
        foreach (var pair in satelliteObjectMap)
        {
            GameObject satellite = pair.Key;
            GameObject assignedObject = pair.Value;

            if (satellite != null && assignedObject != null)
            {
                assignedObject.transform.position = satellite.transform.position;
                assignedObject.transform.localScale = satellite.transform.localScale * 10f;
            }
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
