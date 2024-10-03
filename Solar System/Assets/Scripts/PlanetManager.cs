using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    private const float scale = 10000f;
    private const float minRocheuse = 22578 / scale;
    private const float maxRocheuse = 15868 / scale;
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


    public Transform centerObject;
    public GameObject[] rockyPlanets;
    public GameObject[] gasPlanets;
    public float numberOfPlanets;


    void Start()
    {
        spawnPosition = new Vector3(0, 0, 0);
        GeneratePlanet();
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


        // set the planet inclination
        float inclination = Random.Range(minInclination, maxInclination);
        // tilt the planet
        generatedPlanet.transform.Rotate(Vector3.forward, inclination);
        nGeneratedPlanets++;
        generatedPlanet.name = "GeneratedPlanet" + nGeneratedPlanets.ToString();
        // add the planet to the list of generated planets
        generatedPlanets = new GameObject[] { generatedPlanet };
        // add the rotation manager script to orbit the planet and rotate on its axis
        generatedPlanet.AddComponent<RotationManager>();
        // print the list
        foreach (GameObject planet in generatedPlanets)
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
    }

    

}