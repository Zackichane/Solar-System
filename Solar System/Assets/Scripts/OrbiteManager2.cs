using System.Collections.Generic;
using UnityEngine;

public class OrbitManager2 : MonoBehaviour
{
    public float orbitSpeed = 10f;  // Speed at which planets move around the sun
    public float scale = 10000f;  // Scale to control the size of the orbits

    private GameObject sun;
    private float minRocheuse;
    private float maxRocheuse;
    private float minGazeuse;
    private float maxGazeuse;
    private List<GameObject> rockyPlanets = new List<GameObject>();
    private List<GameObject> gasPlanets = new List<GameObject>();

    void Start()
    {
        // Find the sun by its name
        sun = GameObject.Find("GeneratedSun");

        // Setting scale for orbit distances based on user input
        minRocheuse = 22578 / scale;
        maxRocheuse = 15868 / scale;
        minGazeuse = 44254 / scale;
        maxGazeuse = 482386 / scale;

        // Find all planets with tag "GeneratedPlanet"
        GameObject[] planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");

        // Classify planets into rocky and gas based on their type or scale
        foreach (GameObject planet in planets)
        {
            if (planet.transform.localScale.x < minGazeuse)  // Assuming rocky planets are smaller
            {
                rockyPlanets.Add(planet);
            }
            else
            {
                gasPlanets.Add(planet);
            }
        }

        // Set initial orbits for rocky planets
        SetOrbitDistances(rockyPlanets, minRocheuse, maxRocheuse);

        // Set initial orbits for gas planets
        SetOrbitDistances(gasPlanets, minGazeuse, maxGazeuse);
    }

    void Update()
    {
        // Move planets in their orbits
        MovePlanets(rockyPlanets);
        MovePlanets(gasPlanets);
    }

    private void SetOrbitDistances(List<GameObject> planets, float minDistance, float maxDistance)
    {
        float distanceIncrement = (maxDistance - minDistance) / planets.Count;

        for (int i = 0; i < planets.Count; i++)
        {
            float orbitDistance = maxDistance + (i * distanceIncrement);
            Vector3 direction = (planets[i].transform.position - sun.transform.position).normalized;
            planets[i].transform.position = sun.transform.position + direction * orbitDistance;
        }
    }

    private void MovePlanets(List<GameObject> planets)
    {
        foreach (GameObject planet in planets)
        {
            planet.transform.RotateAround(sun.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
}
