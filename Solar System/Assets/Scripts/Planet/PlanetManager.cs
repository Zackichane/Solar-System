using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;


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

    private const double minMassVenus = 2700000000000000000000000d;
    private const double maxMassVenus = 6500000000000000000000000d;
    private const double minMassMercure = 110000000000000000000000d;
    private const double maxMassMercure = 3000000000000000000000000d;
    private const double minMassMars = 1600000000000000000000000d;
    private const double maxMassMars = 3500000000000000000000000d;
    private const double minMassRocheuse = 4800000000000000000000000d;
    private const double maxMassRocheuse = 10000000000000000000000000d;
    private double randomMassKg;

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

    private static readonly string[] prefixes = {
        "Kepler", "Gliese", "Proxima", "Eris", "Atlas", "Hydra", 
        "Lyra", "Draconis", "Orion", "Nyx", "Helios", "Chronos", 
        "Aquila", "Pegasus", "Vega", "Rigel", "Triton", "Altair", 
        "Cygnus", "Nova", "Zenith", "Aurora", "Titan", "Arcturus"
    };

    private static readonly string[] suffixes = {
        "-A", "-B", "-C", "-D", "-E", "-F", "-G", "-H", "-I", "-J", 
        "-X", "-Y", "-Z", "-1a", "-1b", "-1c", "-2a", "-2b", "-2c", 
        "-10", "-11", "-12", "-20", "-30", "-100", "-200", "-300", "-400"
    };

    private string planetName;

    void Awake()
    {
        // Generate a random planet name by combining a random prefix and suffix
        planetName = GenerateRandomName();
    }

    private string GenerateRandomName()
    {
        string prefix = prefixes[Random.Range(0, prefixes.Length)];
        string suffix = suffixes[Random.Range(0, suffixes.Length)];
        return prefix + suffix;
    }

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
        float randomOrbitDistance = Random.Range(5, 13);
        float randomOrbitOtherPlanets = Random.Range(70, 180);

        for (int i = 0; i < numberOfPlanets; i++)
        {
            if (nGeneratedPlanets == 0)
            {
                currentOrbitDistance += maxSize * randomOrbitDistance;
            }
            else
            {
                currentOrbitDistance += (maxSize * 8) + (orbitBuffer * randomOrbitOtherPlanets);
            }

            float Albedo = Random.Range(0.1f, 1f);

            float starTemperature = (StarGeneration.starTemperature);

            // Calculate the planet's temperature
            float planetTemperature = (starTemperature) * Mathf.Pow((StarGeneration.starSize * 10000 / 2f) / (2 *   currentOrbitDistance * 10000), 0.5f) * Mathf.Pow(1f - Albedo, 0.25f);
            bool isHabitable = false;
            float randomSizeKm;

            if (habitableZoneInnerRadius <= currentOrbitDistance && currentOrbitDistance <= habitableZoneOuterRadius && planetTemperature >= 273 && planetTemperature <= 388)
            {
                randomSizeKm = Random.Range(minRocheuse, maxRocheuse);
                randomMassKg = (double)Random.Range((float)minMassRocheuse, (float)maxMassRocheuse);
                listOfPlanets = rockyPlanets;
                randomPlanetType = "RockyPlanets";
                isHabitable = true;
            }
            else
            {
                // get a random type of planet and its infos
                (randomSizeKm, listOfPlanets, randomPlanetType, randomMassKg) = GetRandomPlanetType();
            }

            // get a random prefab from the list of planets
            GameObject planetPrefab = listOfPlanets[Random.Range(0, listOfPlanets.Length)];
            generatedPlanet = Instantiate(planetPrefab, spawnPosition, Quaternion.identity);
            
            // set the scale of the planet
            generatedPlanet.transform.localScale = new Vector3(randomSizeKm, randomSizeKm, randomSizeKm);

            // set the position of the planet
            generatedPlanet.transform.position = new Vector3(currentOrbitDistance, 0, 0);

            // set the inclination of the planet
            float inclination = Random.Range(minInclination, maxInclination);
            generatedPlanet.transform.Rotate(Vector3.forward, inclination);

            // Generate a unique name for each planet
            string uniquePlanetName = GenerateRandomName();

            // rename the planet with the unique name
            generatedPlanet.name = uniquePlanetName;

            // add components to the planet
            generatedPlanetsList.Add(generatedPlanet);
            generatedPlanet.AddComponent<PlanetRotationManager>();
            generatedPlanet.tag = "GeneratedPlanet";

            // save the planet type
            var planetTypeComponent = generatedPlanet.AddComponent<planetInfos>();

            planetTypeComponent.planetName = uniquePlanetName; // Assign the unique name
            planetTypeComponent.planetType = randomPlanetType;
            planetTypeComponent.planetRadius = (randomSizeKm / 2 * scale).ToString();
            planetTypeComponent.distPlanetStar = (currentOrbitDistance * scale).ToString("E3");
            planetTypeComponent.planetMass = randomMassKg.ToString("E2");
            planetTypeComponent.redSphere = InstantiateRedSpheres(generatedPlanet);

            // convert habitableZoneInnerRadius to float
            habitableZoneInnerRadius = float.Parse(habitableZoneInnerRadius.ToString());

            if (isHabitable)
            {
                planetTypeComponent.planetHabitable = "yes";
            }
            else
            {
                planetTypeComponent.planetHabitable = "no";
            }

            planetTypeComponent.planetTemperature = planetTemperature.ToString();
            nGeneratedPlanets++;
        }
        generatedPlanets = generatedPlanetsList.ToArray();
        planetsWereGenerated = true;
    }

    // function to get a random planet type and its infos
    (float, GameObject[], string, double) GetRandomPlanetType()
    {
        int randomType = Random.Range(0, 4);
        float randomSizeKm = 0f;
        double randomMassKg = 0d;
        GameObject[] listOfPlanets;
        string randomPlanetType;

        if (randomType == 0)
        {
            randomSizeKm = Random.Range(minMercure, maxMercure);
            randomMassKg = (double)Random.Range((float)minMassMercure, (float)maxMassMercure);
            listOfPlanets = MercuryPlanets;
            randomPlanetType = "MercuryPlanets";
        }
        else if (randomType == 1)
        {
            randomSizeKm = Random.Range(minVenus, maxVenus);
            randomMassKg = (double)Random.Range((float)minMassVenus, (float)maxMassVenus);
            listOfPlanets = VenusPlanets;
            randomPlanetType = "VenusPlanets";
        }
        else if (randomType == 2)
        {
            randomSizeKm = Random.Range(minMars, maxMars);
            randomMassKg = (double)Random.Range((float)minMassMars, (float)maxMassMars);
            listOfPlanets = MarsPlanets;
            randomPlanetType = "MarsPlanets";
        }
        else
        {
            randomSizeKm = Random.Range(minGazeuse, maxGazeuse);
            listOfPlanets = gasPlanets;
            randomPlanetType = "GasPlanets";
        }
        // round the randomMassKg to 2 decimals so it will be like : 2.00 E24
        randomMassKg = System.Math.Round(randomMassKg, 2);
        return (randomSizeKm, listOfPlanets, randomPlanetType, randomMassKg);
    }

    GameObject InstantiateRedSpheres(GameObject planet)
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
        return redSphere;
    }

}
