using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerate : MonoBehaviour
{
    private const float scale = 10000f;

    private const float minRocheuse = 22578 / scale;
    private const float maxRocheuse = 15868 / scale;
    private const float minGazeuse = 44254 / scale;
    private const float maxGazeuse = 482386 / scale;


    // Rotation speed in degrees per second
    public float rotationSpeed = 1f;

    // Reference to the object around which the planet will rotate
    public Transform centerObject;

    // Prefab for the planet
    public GameObject planetPrefab;

    // get the position of the Planet object
    GameObject planetEmpty;
    float PlanetPos;
    private Vector3 spawnPosition;
    public Camera mainCamera;

    // Reference to the generated planet
    private GameObject generatedPlanet;

    void Start()
    {
        planetEmpty = GameObject.Find("Planet");
        PlanetPos = planetEmpty.transform.position.x;
        Debug.Log(PlanetPos);
        spawnPosition = new Vector3(PlanetPos, 0, 0);
        // print the spawn position
        Debug.Log(spawnPosition);
        GeneratePlanet();
    }

    void Update()
    {
        RotatePlanet();
        RotatePlanetOnAxis();
    }

    void GeneratePlanet()
    {
        // choose random between rocheuse and gazeuse
        float randomType = Random.Range(0, 2);
        float randomSizeKm;
        if (randomType == 1)
        {
            randomSizeKm = Random.Range(minRocheuse, maxRocheuse);
        }
        else
        {
            randomSizeKm = Random.Range(minGazeuse, maxGazeuse);
        }

        // Instantiate the planet at the specified spawn position with random size
        generatedPlanet = Instantiate(planetPrefab, spawnPosition, Quaternion.identity);

        // Apply the size (localScale) based on the random size
        generatedPlanet.transform.localScale = new Vector3(randomSizeKm, randomSizeKm, randomSizeKm);

        generatedPlanet.name = "GeneratedPlanet";

        // move the mainCamera
        //mainCamera.transform.position = new Vector3(spawnPosition.x * 2, 0, 0);
    }

    void RotatePlanet()
    {
        if (centerObject != null)
        {
            // Rotate the planet around the centerObject's position
            generatedPlanet.transform.RotateAround(centerObject.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    // rotate the planet on its own axis
    void RotatePlanetOnAxis()
    {
        if (generatedPlanet != null)
        {
            // Rotate the planet around its Y-axis
            generatedPlanet.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}