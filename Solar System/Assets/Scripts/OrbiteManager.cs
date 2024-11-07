using System.Collections.Generic;
using UnityEngine;

public class OrbitManager : MonoBehaviour
{
    private float minRocheuse;
    private float maxRocheuse;
    private float minGazeuse;
    private float maxGazeuse;
    private float orbitSpacing = 50f;  // General increment between each orbit
    private float rockyGasGapMultiplier = 10f;  // Large gap multiplier between rocky and gas planets
    private float orbitDistanceMultiplier = 5f;  // Initial distance multiplier for rocky planets

    private List<GameObject> rockyPlanets = new List<GameObject>();
    private List<GameObject> gasPlanets = new List<GameObject>();
    private GameObject[] planets;
    private bool typeGet = false;

    void Start()
    {
        float scale = 10000f;
        minRocheuse = 22578 / scale;
        maxRocheuse = 15868 / scale;
        minGazeuse = 44254 / scale;
        maxGazeuse = 482386 / scale;
    }

    void Update()
    {
        if (!typeGet)
        {
            GetAllPlanets();
        }
    }

    void GetAllPlanets()
    {
        planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
        if (planets.Length > 0)
        {
            typeGet = true;
            GetPlanetsType();
        }
    }

    void GetPlanetsType()
    {
        foreach (GameObject planet in planets)
        {
            float size = planet.transform.localScale.x;

            if (size >= minRocheuse && size <= maxRocheuse)
            {
                rockyPlanets.Add(planet);
            }
            else if (size >= minGazeuse && size <= maxGazeuse)
            {
                gasPlanets.Add(planet);
            }
            else
            {
                rockyPlanets.Add(planet);
            }
        }

        GenerateOrbits();
    }

    void GenerateOrbits()
    {
        GameObject star = GameObject.FindGameObjectWithTag("GeneratedStar");
        if (star == null)
        {
            return;
        }

        float starSize = star.transform.localScale.x;
        float rockyDistance = starSize * orbitDistanceMultiplier;  // Starting distance for rocky planets
        float gasDistance = rockyDistance * rockyGasGapMultiplier;  // Starting distance for gas planets

        // Create orbits for rocky planets
        foreach (GameObject rockyPlanet in rockyPlanets)
        {
            rockyDistance += rockyPlanet.transform.localScale.x * orbitSpacing;

            // Create an orbit GameObject and set it up
            GameObject orbit = new GameObject("Orbit" + rockyPlanet.name);
            orbit.transform.position = new Vector3(rockyDistance, 0, 0);

            // Set the planet's position along the x-axis within the orbit
            rockyPlanet.transform.localPosition = orbit.transform.position;
        }

        // Create orbits for gas planets with a large initial distance
        foreach (GameObject gasPlanet in gasPlanets)
        {
            gasDistance += gasPlanet.transform.localScale.x * orbitSpacing;

            // Create an orbit GameObject and set it up
            GameObject orbit = new GameObject("Orbit" + gasPlanet.name);
            orbit.transform.parent = star.transform;
            orbit.transform.localScale = new Vector3(gasDistance, gasDistance, gasDistance);
            orbit.transform.position = star.transform.position;

            // Set the planet's position along the x-axis within the orbit
            gasPlanet.transform.parent = orbit.transform;
            gasPlanet.transform.localPosition = new Vector3(gasDistance / 2, 0, 0);  // Move the planet to the edge of the orbit
        }
    }
}
