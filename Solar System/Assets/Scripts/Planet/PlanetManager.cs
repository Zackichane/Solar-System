using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    private const float scale = 10000f;
    private const float minVenus = 14000 / scale;
    private const float maxVenus = 21000 / scale;
    private const float minMercure = 6000 / scale;
    private const float maxMercure = 10000 / scale;
    private const float minMars = 9000 / scale;
    private const float maxMars = 16000 / scale;
    private const float minRocheuse = 15800 / scale;
    private const float maxRocheuse = 22500 / scale;
    private const float minGazeuse = 44200 / scale;
    private const float maxGazeuse = 482000 / scale;
    private Vector3 spawnPosition;
    private float maxInclination = 40f;
    private float minInclination = 0f;
    private GameObject generatedPlanet;
    private GameObject[] listOfPlanets;
    private float nGeneratedPlanets = 0;
    private GameObject[] generatedPlanets;
    private List<float> planetSizes = new List<float>();
    private List<GameObject> generatedPlanetsList = new List<GameObject>();
    public float starSize;
    public float orbitBuffer = 100f;

    public Transform centerObject;
    public GameObject[] MercuryPlanets;
    public GameObject[] VenusPlanets;
    public GameObject[] MarsPlanets;
    public GameObject[] rockyPlanets;
    public GameObject[] gasPlanets;
    public GameObject redSpherePrefab; // Prefab for the red spheres
    private float numberOfPlanets;
    private float minPlanet;
    private float maxPlanet;
    private string randomPlanetType;

    void Start()
    {
        spawnPosition = new Vector3(0, 0, 0);
        minPlanet = PlayerPrefs.GetInt("minPlanet");
        maxPlanet = PlayerPrefs.GetInt("maxPlanet");
        numberOfPlanets = Mathf.Round(Random.Range(minPlanet, maxPlanet));

        GameObject star = GameObject.FindGameObjectWithTag("GeneratedStar");
        if (star != null)
        {
            starSize = star.transform.localScale.x;
        }

        GeneratePlanet();
        ArrangePlanetsInOrbits();
    }

    void GeneratePlanet()
    {
        float randomType = Random.Range(0, 5);
        float randomSizeKm;

        if (randomType == 0)
        {
            randomSizeKm = Random.Range(minMercure, maxMercure);
            listOfPlanets = MercuryPlanets;
            randomPlanetType = "MercuryPlanets";
        }
        else if (randomType == 1)
        {
            randomSizeKm = Random.Range(minVenus, maxVenus);
            listOfPlanets = VenusPlanets;
            randomPlanetType = "VenusPlanets";
        }
        else if (randomType == 2)
        {
            randomSizeKm = Random.Range(minMars, maxMars);
            listOfPlanets = MarsPlanets;
            randomPlanetType = "MarsPlanets";
        }
        else if (randomType == 3)
        {
            randomSizeKm = Random.Range(minRocheuse, maxRocheuse);
            listOfPlanets = rockyPlanets;
            randomPlanetType = "RockyPlanets";
        }
        else
        {
            randomSizeKm = Random.Range(minGazeuse, maxGazeuse);
            listOfPlanets = gasPlanets;
            randomPlanetType = "GasPlanets";
        }

        GameObject planetPrefab = listOfPlanets[Random.Range(0, listOfPlanets.Length)];
        generatedPlanet = Instantiate(planetPrefab, spawnPosition, Quaternion.identity);
        generatedPlanet.transform.localScale = new Vector3(randomSizeKm, randomSizeKm, randomSizeKm);
        planetSizes.Add(randomSizeKm);

        float inclination = Random.Range(minInclination, maxInclination);
        generatedPlanet.transform.Rotate(Vector3.forward, inclination);
        nGeneratedPlanets++;
        generatedPlanet.name = "GeneratedPlanet" + nGeneratedPlanets.ToString();
        generatedPlanetsList.Add(generatedPlanet);
        generatedPlanet.AddComponent<PlanetRotationManager>();
        generatedPlanet.tag = "GeneratedPlanet";

        var planet_component = generatedPlanet.AddComponent<planetType>();
        planet_component.planet_type = randomPlanetType;

        if (nGeneratedPlanets < numberOfPlanets)
        {
            GeneratePlanet();
        }
        else
        {
            generatedPlanets = generatedPlanetsList.ToArray();
        }
    }

    void ArrangePlanetsInOrbits()
{
    float currentOrbitDistance = starSize * 2;
    float largestPlanetSize = 0f;

    foreach (float size in planetSizes)
    {
        if (size > largestPlanetSize)
        {
            largestPlanetSize = size;
        }
    }

    for (int i = 0; i < generatedPlanets.Length; i++)
    {
        GameObject planet = generatedPlanets[i];
        float planetSize = planetSizes[i];

        if (planet.GetComponent<planetType>().planet_type == "GasPlanets")
        {
            currentOrbitDistance += (largestPlanetSize * 3) + (orbitBuffer * 2);
        }
        else
        {
            currentOrbitDistance += (largestPlanetSize * 2) + orbitBuffer;
        }

        planet.transform.position = new Vector3(currentOrbitDistance, 0, 0);

        // Instantiate and scale the red sphere
        GameObject redSphere = Instantiate(redSpherePrefab, planet.transform.position, Quaternion.identity);
        
        // Check planet type and apply appropriate scale multiplier
        if (planet.GetComponent<planetType>().planet_type == "RockyPlanets" ||
            planet.GetComponent<planetType>().planet_type == "MarsPlanets" ||
            planet.GetComponent<planetType>().planet_type == "MercuryPlanets" ||
            planet.GetComponent<planetType>().planet_type == "VenusPlanets")
        {
            redSphere.transform.localScale = planet.transform.localScale * 20f; // 10x the size of the planet
        }
        else
        {
            redSphere.transform.localScale = planet.transform.localScale * 5f; // 5x the size of the planet
        }

        redSphere.transform.parent = planet.transform; // Make the red sphere follow the planet
    }
}

}
