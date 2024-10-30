using System.Collections.Generic;
using UnityEngine;

public class OrbitManager : MonoBehaviour
{
    public float minRocheuse;
    public float maxRocheuse;
    public float minGazeuse;
    public float maxGazeuse;

    private List<GameObject> rockyPlanets = new List<GameObject>();
    private List<GameObject> gasPlanets = new List<GameObject>();
    private GameObject[] planets;
    private bool typeGet = false;

    void Start()
    {
        // Initialize min and max sizes
        float scale = 10000f;
        minRocheuse = 22578 / scale;
        maxRocheuse = 15868 / scale;
        minGazeuse = 44254 / scale;
        maxGazeuse = 482386 / scale;
    }

    void Update()
    {
        if (typeGet == false)
        {
            GetAllPlanets();
        }
    }

    void GetAllPlanets()
    {
        planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
        print("Planets: " + planets);
        int nPlanets = planets.Length;
        if (nPlanets > 0)
        {
            typeGet = true;
            print("going to generate the orbits");
            GetPlanetsType();
        }
    }

    void GetPlanetsType()
    {
        foreach (GameObject planet in planets)
        {
            // get the planet type with the scale
            float size = planet.transform.localScale.x;

            if (size >= minRocheuse && size <= maxRocheuse)
            {
                // is a rocky planet
                rockyPlanets.Add(planet);
            }
            else if (size >= minGazeuse && size <= maxGazeuse)
            {
                // is a gas planet
                gasPlanets.Add(planet);
            }
        }

        GenerateOrbits();
    }

    void GenerateOrbits()
    {
        GameObject star = GameObject.FindGameObjectWithTag("GeneratedStar");
        if (star == null)
        {
            Debug.LogError("Star not found!");
            return;
        }

        print("Star: " + star.name);
        float starSize = star.transform.localScale.x;
        float distance = starSize;

        // create the orbits for rocky planets
        foreach (GameObject rockyPlanet in rockyPlanets)
        {
            print("Rocky planet orbit: " + rockyPlanet.name);
            distance += rockyPlanet.transform.localScale.x * 2;
            GameObject orbit = new GameObject("Orbit" + rockyPlanet.name);
            print("Orbit: " + orbit.name);
            orbit.transform.parent = star.transform;
            orbit.transform.localScale = new Vector3(distance, distance, distance);
            orbit.transform.position = star.transform.position;
            rockyPlanet.transform.parent = orbit.transform;
        }

        // create the orbits for gas planets
        foreach (GameObject gasPlanet in gasPlanets)
        {
            print("Gas planet orbit: " + gasPlanet.name);
            distance += gasPlanet.transform.localScale.x * 10;
            GameObject orbit = new GameObject("Orbit" + gasPlanet.name);
            print("Orbit: " + orbit.name);
            orbit.transform.parent = star.transform;
            orbit.transform.localScale = new Vector3(distance, distance, distance);
            orbit.transform.position = star.transform.position;
            gasPlanet.transform.parent = orbit.transform;
        }
    }
}