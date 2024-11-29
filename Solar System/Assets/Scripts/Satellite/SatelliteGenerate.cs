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
    public float orbitBuffer = 2f; // Increased buffer distance to avoid collisions
    private int satelliteCounter = 0;

    public List<GameObject> satellites = new List<GameObject>(); // The satellites prefabs
    public List<GameObject> satellitesArray; // The satellites prefabs
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
                    int maxSatellites = 5;
                    int numSatellites = Random.Range(minSatellites, maxSatellites + 1);
                    if (numSatellites > 0)
                    {
                        GenerateSatellites(numSatellites, generatedPlanet[i]);
                    }
                }
            }
        }
    }

    void GenerateSatellites(int numSatellites, GameObject planetObject)
    {
        float currentOrbit = 0;
        float planetRadius = planetObject.transform.localScale.x / 2;
        float largestSatelliteSize = 0f;

        foreach (GameObject satellite in satellites)
        {
            float satelliteSize = satellite.transform.localScale.x;
            if (satelliteSize > largestSatelliteSize)
            {
                largestSatelliteSize = satelliteSize;
            }
        }

        for (int i = 0; i < numSatellites; i++)
        {
            currentOrbit += largestSatelliteSize + orbitBuffer;
            float randomBuffer = Random.Range(2f, 5f);

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
            float minSize, maxSize;
            (minSize, maxSize) = GetSatelliteSize(planetObject);

            float sizePlanet = planetObject.transform.localScale.x;
            float sizeSatellite = Random.Range(minSize, maxSize);

            float distance = sizePlanet * 2f;
            float angle = satelliteCounter * 30f;
            Vector3 spawnPosition = planetObject.transform.position + new Vector3(currentOrbit, 0, 0);

            generatedSatellite = Instantiate(satellitePrefab, spawnPosition, Quaternion.identity);
            // add the satellite to the list of satellites
            satellitesArray.Add(generatedSatellite);
            generatedSatellite.transform.localScale = new Vector3(sizeSatellite, sizeSatellite, sizeSatellite);

            generatedSatellite.name = "GeneratedSatellite" + satelliteCounter++;
            generatedSatellite.tag = "GeneratedSatellite";

            generatedSatellite.AddComponent<planetTracker>();
            generatedSatellite.GetComponent<planetTracker>().planet = planetObject;
            
            generatedSatellite.GetComponent<planetTracker>().blueSphere = InstantiateBlueSpheres(generatedSatellite);

            generatedSatellite.AddComponent<SatelliteRotationManager>();

            // Set the satellite as a child of the planet object
            generatedSatellite.transform.parent = planetObject.transform;
        }
    }

    (float, float) GetSatelliteSize(GameObject planetObject)
    {
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
        return (minSize, maxSize);
    }

    GameObject InstantiateBlueSpheres(GameObject satellite)
    {
        GameObject blueSphere = Instantiate(blueSpherePrefab, satellite.transform.position, Quaternion.identity);
        blueSphere.transform.localScale = new Vector3(20, 20, 20);
        blueSphere.transform.parent = satellite.transform;
        return blueSphere;
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
