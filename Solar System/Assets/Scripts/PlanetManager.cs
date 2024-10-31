using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    private const float scale = 10000f;
    private const float minRocheuse = 15868 / scale;
    private const float maxRocheuse = 22578 / scale;
    private const float minGazeuse = 44254 / scale;
    private const float maxGazeuse = 482386 / scale;
    private bool stopOrbite = false;
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
    public GameObject[] rockyPlanets;
    public GameObject[] gasPlanets;
    private float numberOfPlanets;
    private float minPlanet;
    private float maxPlanet;


    void Start()
    {
        spawnPosition = new Vector3(0, 0, 0);
        minPlanet = PlayerPrefs.GetInt("minPlanet");
        maxPlanet = PlayerPrefs.GetInt("maxPlanet");
        // get a random int between minPlanet and maxPlanet
        numberOfPlanets = Random.Range(minPlanet, maxPlanet);
        // round the number of planets
        numberOfPlanets = Mathf.Round(numberOfPlanets);
        print("Number of planets: " + numberOfPlanets);

        // Get the star with the tag "GeneratedStar" and its size
        GameObject star = GameObject.FindGameObjectWithTag("GeneratedStar");
        if (star != null)
        {
            starSize = star.transform.localScale.x; // Assuming uniform scale
            print("Star size: " + starSize);
        }
        else
        {
            Debug.LogError("Star with tag 'GeneratedStar' not found!");
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
        float randomType = Random.Range(0, 2);
        float randomSizeKm;
        if (randomType == 1)
        {
            randomSizeKm = Random.Range(minRocheuse, maxRocheuse);
            // the list is the rocky planets
            listOfPlanets = rockyPlanets;
        }
        else
        {
            randomSizeKm = Random.Range(minGazeuse, maxGazeuse);
            // the list is the gas planets
            listOfPlanets = gasPlanets;
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
        // print the list
        foreach (GameObject planet in generatedPlanetsList)
        {
            Debug.Log(planet.name);
        }
        // add the planet tag to the generated planet
        generatedPlanet.tag = "GeneratedPlanet";
        // generate a camera for the planet
        GameObject camera = new GameObject();
        camera.AddComponent<Camera>();
        camera.AddComponent<CamObjFollow>();
        // change a variable form the CamObjFollow script
        camera.GetComponent<CamObjFollow>().targetName = generatedPlanet.name;
        camera.GetComponent<CamObjFollow>().secondTargetName = "GeneratedSatellite" + nGeneratedPlanets.ToString();
        // rename the camera
        camera.name = "Camera" + generatedPlanet.name;
        // add the tag "MainCamera" to the camera
        camera.tag = "MainCamera";
        // deactivate the camera component
        camera.GetComponent<Camera>().enabled = false;

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

            // Calculate orbit distance based on the largest planet size with buffer
            currentOrbitDistance += largestPlanetSize * 2 + orbitBuffer;

            // Set the planet's position on its orbit
            planet.transform.position = new Vector3(currentOrbitDistance, 0, 0);

            // Print the orbit coordinates
            print($"Planet {planet.name} orbit coordinates: {planet.transform.position}");

            // Increase orbit distance for the next planet
            currentOrbitDistance += largestPlanetSize * 2 + orbitBuffer;
        }
    }

}