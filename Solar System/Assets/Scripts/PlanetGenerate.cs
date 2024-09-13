using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerate : MonoBehaviour
{
    // Minimum and maximum sizes in kilometers (scaled to Unity units)
    private const float minSizeKm = 5f; // Example minimum size
    private const float maxSizeKm = 40f; // Example maximum size

    // Rotation speed in degrees per second
    public float rotationSpeed = 10f;

    // Reference to the object around which the planet will rotate
    public Transform centerObject;

    // Prefab for the planet
    public GameObject planetPrefab;

    // Position to spawn the planet
    public Vector3 spawnPosition;

    // Reference to the generated planet
    private GameObject generatedPlanet;

    void Start()
    {
        GeneratePlanet();
    }

    void Update()
    {
        RotatePlanet();
        RotatePlanetOnAxis();
    }

    void GeneratePlanet()
    {
        // Generate a random size in kilometers and convert to Unity units
        float randomSizeKm = Random.Range(minSizeKm, maxSizeKm);
        float randomSizeUnity = randomSizeKm;

        // Instantiate the planet at the specified spawn position with random size
        generatedPlanet = Instantiate(planetPrefab, spawnPosition, Quaternion.identity);

        // Apply the size (localScale) based on the random size
        generatedPlanet.transform.localScale = new Vector3(randomSizeUnity, randomSizeUnity, randomSizeUnity);
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