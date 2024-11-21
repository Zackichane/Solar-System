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
    private List<GameObject> generatedPlanetsList = new List<GameObject>(); // Use a list to store generated planets
    public float starSize; // Example star size, adjust as needed
    public float orbitBuffer = 100f; // Base buffer distance to avoid collisions


    public Transform centerObject;
    public GameObject[] MercuryPlanets;
    public GameObject[] VenusPlanets;
    public GameObject[] MarsPlanets;
    public GameObject[] rockyPlanets;
    public GameObject[] gasPlanets;
    private float numberOfPlanets;
    private float minPlanet;
    private float maxPlanet;
    private string randomPlanetType;


    void Start()
    {
        spawnPosition = new Vector3(0, 0, 0);
        minPlanet = PlayerPrefs.GetInt("minPlanet");
        maxPlanet = PlayerPrefs.GetInt("maxPlanet");
        // get a random int between minPlanet and maxPlanet
        numberOfPlanets = Random.Range(minPlanet, maxPlanet);
        // round the number of planets
        numberOfPlanets = Mathf.Round(numberOfPlanets);

        // Get the star with the tag "GeneratedStar" and its size
        GameObject star = GameObject.FindGameObjectWithTag("GeneratedStar");
        if (star != null)
        {
            starSize = star.transform.localScale.x; // Assuming uniform scale
        }

        GeneratePlanet();
        ArrangePlanetsInOrbits();
    }

    void Update()
    {
        
    }

    void GeneratePlanet()
    {
        // choose random between rocheuse and gazeuse
        float randomType = Random.Range(0, 5);
        float randomSizeKm;
        if (randomType == 0)
        {
            randomSizeKm = Random.Range(minMercure, maxMercure);
            // the list is the rocky planets
            listOfPlanets = MercuryPlanets;
            randomPlanetType = "MercuryPlanets";
        }
        else if (randomType == 1)
        {
            randomSizeKm = Random.Range(minVenus, maxVenus);
            // the list is the rocky planets
            listOfPlanets = VenusPlanets;
            randomPlanetType = "VenusPlanets";
        }
        else if (randomType == 2)
        {
            randomSizeKm = Random.Range(minMars, maxMars);
            // the list is the rocky planets
            listOfPlanets = MarsPlanets;
            randomPlanetType = "MarsPlanets";
        }
        else if (randomType == 3)
        {
            randomSizeKm = Random.Range(minRocheuse, maxRocheuse);
            // the list is the rocky planets
            listOfPlanets = rockyPlanets;
            randomPlanetType = "RockyPlanets";
        }
        else
        {
            randomSizeKm = Random.Range(minGazeuse, maxGazeuse);
            // the list is the gas planets
            listOfPlanets = gasPlanets;
            randomPlanetType = "GasPlanets";
        }

        // Choose a random planet from the list
        GameObject planetPrefab = listOfPlanets[Random.Range(0, listOfPlanets.Length)];

        // Instantiate the planet at the specified spawn position with random size
        generatedPlanet = Instantiate(planetPrefab, spawnPosition, Quaternion.identity);

        // Apply the size (localScale) based on the random size
        generatedPlanet.transform.localScale = new Vector3(randomSizeKm, randomSizeKm, randomSizeKm);

        // Store the size of the planet
        planetSizes.Add(randomSizeKm);

        // set the planet inclination
        float inclination = Random.Range(minInclination, maxInclination);
        // tilt the planet
        generatedPlanet.transform.Rotate(Vector3.forward, inclination);
        nGeneratedPlanets++;
        generatedPlanet.name = "GeneratedPlanet" + nGeneratedPlanets.ToString();
        // add the planet to the list of generated planets
        generatedPlanetsList.Add(generatedPlanet);
        // add the rotation manager script to orbit the planet and rotate on its axis
        generatedPlanet.AddComponent<PlanetRotationManager>();

        // add the planet tag to the generated planet
        generatedPlanet.tag = "GeneratedPlanet";

        var planet_component = generatedPlanet.AddComponent<planetType>();
        planet_component.planet_type = randomPlanetType;
        

        if (nGeneratedPlanets < numberOfPlanets)
        {
            GeneratePlanet();
        }
        else
        {
            // Convert the list to an array once all planets are generated
            generatedPlanets = generatedPlanetsList.ToArray();
        }
    }

void ArrangePlanetsInOrbits()
{
    float currentOrbitDistance = starSize * 2; // Start orbit distance based on star size
    float largestPlanetSize = 0f;

    // Find the largest planet size
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

        // Calculate orbit distance with extra buffer for gas planets
        if (planet.GetComponent<planetType>().planet_type == "GasPlanet") // Assuming gas planets have this type
        {
            currentOrbitDistance += (largestPlanetSize * 3) + (orbitBuffer * 2); // Extra buffer for gas planets
        }
        else
        {
            currentOrbitDistance += (largestPlanetSize * 2) + orbitBuffer; // Normal buffer for other planets
        }

        // Set the planet's position on its orbit
        planet.transform.position = new Vector3(currentOrbitDistance, 0, 0);

        // Increase orbit distance for the next planet
        currentOrbitDistance += largestPlanetSize * 2 + orbitBuffer;
    }
}

}