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
    private GameObject[] generatedPlanets = null;
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
    public GameObject habitableZone;
    private float habitableZoneInnerRadius;
    private float habitableZoneOuterRadius;
    private bool planetsWereGenerated = false;

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

        // Start the coroutine to wait and then check the habitable zone
        StartCoroutine(CheckHabitableZone());
    }

    IEnumerator CheckHabitableZone()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(1);

        // Get the habitable zone inner and outer radius
        habitableZoneInnerRadius = habitableZone.GetComponent<SimpleRingGenerator>().innerRadius;
        habitableZoneOuterRadius = habitableZone.GetComponent<SimpleRingGenerator>().outerRadius;

        // If the values are different from 0, generate the planet
        if (habitableZoneInnerRadius != 0 && habitableZoneOuterRadius != 0 && planetsWereGenerated == false)
        {
            GeneratePlanet();
        }
    }

    void GeneratePlanet()
    {
        float currentOrbitDistance = starSize * 2;
        // get the max size of a planet
        float maxSize = Mathf.Max(maxVenus, maxMercure, maxMars, maxRocheuse, maxGazeuse);

        for (int i = 0; i < numberOfPlanets; i++)
        {
            if (nGeneratedPlanets == 0)
            {
                currentOrbitDistance += maxSize * 10;
            }
            else
            {
                currentOrbitDistance += (maxSize * 8) + (orbitBuffer * 30);
            }

            float randomSizeKm;
            if (habitableZoneInnerRadius <= currentOrbitDistance && currentOrbitDistance <= habitableZoneOuterRadius)
            {
                randomSizeKm = Random.Range(minRocheuse, maxRocheuse);
                listOfPlanets = rockyPlanets;
                randomPlanetType = "RockyPlanets";
                Debug.Log("The planet : " + "GeneratedPlanet" + (nGeneratedPlanets + 1).ToString() + " is in the habitable zone");
            }
            else
            {
                // get a random type of planet and its infos
                (randomSizeKm, listOfPlanets, randomPlanetType) = GetRandomPlanetType();
            }

            // get a random prefab from the list of planets
            GameObject planetPrefab = listOfPlanets[Random.Range(0, listOfPlanets.Length)];
            generatedPlanet = Instantiate(planetPrefab, spawnPosition, Quaternion.identity);
            
            // set the scale of the planet
            generatedPlanet.transform.localScale = new Vector3(randomSizeKm, randomSizeKm, randomSizeKm);

            // set the position of the planet
            generatedPlanet.transform.position = new Vector3(currentOrbitDistance, 0, 0);

            // instantiate the red sphere
            InstantiateRedSpheres(generatedPlanet);

            // set the inclination of the planet
            float inclination = Random.Range(minInclination, maxInclination);
            generatedPlanet.transform.Rotate(Vector3.forward, inclination);

            // rename the planet and add some components
            generatedPlanet.name = "GeneratedPlanet" + (nGeneratedPlanets + 1).ToString();
            generatedPlanetsList.Add(generatedPlanet);
            generatedPlanet.AddComponent<PlanetRotationManager>();
            generatedPlanet.tag = "GeneratedPlanet";

            // save the planet type
            var planetTypeComponent = generatedPlanet.AddComponent<planetInfos>();
            planetTypeComponent.planetName = (string)generatedPlanet.name;
            planetTypeComponent.planetType = (string)randomPlanetType;
            planetTypeComponent.planetRadius = (string)(randomSizeKm/2 * scale).ToString();
            planetTypeComponent.planetTemperature = (string)Random.Range(0, 100).ToString(); // try to get a realistic temperature
            planetTypeComponent.planetMass = (string)Random.Range(0, 100).ToString(); // try to get a realistic mass
            planetTypeComponent.distPlanetStar = (string)(currentOrbitDistance*scale).ToString();
            planetTypeComponent.planetHabitable = (string)(habitableZoneInnerRadius <= currentOrbitDistance && currentOrbitDistance <= habitableZoneOuterRadius).ToString();


            nGeneratedPlanets++;
        }
        generatedPlanets = generatedPlanetsList.ToArray();
        planetsWereGenerated = true;
    }

    // function to get a random planet type and its infos
    (float, GameObject[], string) GetRandomPlanetType()
    {
        int randomType = Random.Range(0, 4);
        float randomSizeKm;
        GameObject[] listOfPlanets;
        string randomPlanetType;

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
        else
        {
            randomSizeKm = Random.Range(minGazeuse, maxGazeuse);
            listOfPlanets = gasPlanets;
            randomPlanetType = "GasPlanets";
        }
        return (randomSizeKm, listOfPlanets, randomPlanetType);
    }

    void InstantiateRedSpheres(GameObject planet)
    {
        // Instantiate and scale the red sphere
        GameObject redSphere = Instantiate(redSpherePrefab, planet.transform.position, Quaternion.identity);
        
        if (planet.transform.localScale.x >= 100)
        {
            redSphere.transform.localScale = planet.transform.localScale;
        }
        else
        {
            redSphere.transform.localScale = new Vector3(100, 100, 100);
        }

        redSphere.transform.parent = planet.transform;
    }

}
