using UnityEngine;
using static System.Math;


public class StarGeneration : MonoBehaviour
{
    public GameObject YellowDwarf;  // Prefab of the star
    public GameObject WhiteDwarf;
    public GameObject RedGiant;
    public GameObject BlueGiant;
    public Camera mainCamera;
    private GameObject generatedStar;
    private GameObject starPrefab;
    private GameObject[] listOfPlanets;

    // Minimum and maximum sizes in kilometers (scale : 1 unit = 10 000 km)
    private float minSizeKm;
    private float maxSizeKm;
    private const float scale = 10000f;
    public float rotationSpeed = 1f;
    private float minDist;
    private float maxDist;
    private string planetName = "GeneratedPlanet";
    private bool planetMoved = false;
    private float randomSizeKm;

    void Start()
    {
        // get the starType in the playerprefs that is an int
        int starType = PlayerPrefs.GetInt("starType");

        if (starType == 1)
        {
            starPrefab = WhiteDwarf;
            // 7000 km to 14000 km
            minSizeKm = 7000 / scale;
            maxSizeKm = 14000 / scale;
            minDist = 10;
            maxDist = 50;
        }
        else if (starType == 2)
        {
            starPrefab = YellowDwarf;
            // 1 120 000 km to 1 680 000 km
            minSizeKm = 1120000 / scale;
            maxSizeKm = 1680000 / scale;
            minDist = 50;
            maxDist = 100;

        }
        else if (starType == 3)
        {
            starPrefab = RedGiant;
            // 99 779 000 km to 997 790 000 km
            minSizeKm = 99779000 / scale;
            maxSizeKm = 997790000 / scale;
            minDist = 1000;
            maxDist = 3000;
        }
        else
        {
            starPrefab = BlueGiant;
            // 14 000 000 km to 140 000 000 km
            minSizeKm = 14000000 / scale;
            maxSizeKm = 140000000 / scale;
            minDist = 10000;
            maxDist = 20000;

        }
        GenerateYellowDwarf();
    }

    void Update()
    {
        RotateYellowDwarf();
        if (generatedStar.name != "GeneratedStar") { generatedStar.name = "GeneratedStar"; }
        if (planetMoved == false)
        {
            // add the object with the tag "GeneratedPlanet" to the list of planets
            listOfPlanets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
            foreach (GameObject planet in listOfPlanets)
            {
                // move the planet
                MovePlanet(planet);
            }
        }
    }

    void GenerateYellowDwarf()
    {
        randomSizeKm = Random.Range(minSizeKm, maxSizeKm);

        // Instantiate the star (yellow dwarf) at a position (0, 0, 0) with random size
        generatedStar = Instantiate(starPrefab, Vector3.zero, Quaternion.identity);

        // Apply the size (localScale) based on the random size
        generatedStar.transform.localScale = new Vector3(randomSizeKm, randomSizeKm, randomSizeKm);

        // Add a glow effect (emission) to the material of the star
        Renderer starRenderer = generatedStar.GetComponent<Renderer>();
        if (starRenderer != null)
        {
            // Set the material color to yellow
            starRenderer.material.color = Color.white;

            // Enable emission on the material to make it glow
            starRenderer.material.EnableKeyword("_EMISSION");
            starRenderer.material.SetColor("_EmissionColor", Color.yellow * 0.5f); // Adjust intensity with * 2f
        }

        // Optional: Add a Light component to simulate real light emission
        Light starLight = generatedStar.AddComponent<Light>();
        starLight.color = Color.white;
        starLight.intensity = 10f; // Adjust intensity for the glow
        starLight.range = randomSizeKm * 10f; // Adjust light range based on size

        // rename the genereated star
        generatedStar.name = "GeneratedStar";
        generatedStar.tag = "GeneratedStar";
    }
    void RotateYellowDwarf()
    {
        if (generatedStar != null)
        {
            // Rotate the star around its Y-axis
            generatedStar.transform.Rotate(Vector3.up, rotationSpeed);
        }
    }

    void MovePlanet(GameObject planet)
    {
        float dist = 0f;
        float randomDist = Random.Range(minDist, maxDist);
        float radiusStar = randomSizeKm / 2f;


        float radiusPlanet = planet.transform.localScale.x / 2f;
        if (radiusPlanet > radiusStar)
        {
            float toAdd = radiusPlanet + radiusStar;
            dist = toAdd;
        }
        else
        {
            dist = radiusStar;
        }

        dist = dist + (randomSizeKm / 2f + randomDist);
        // round to 2 decimal places
        dist = (float)Round(dist * 100f) / 100f;
        planet.transform.position = new Vector3(dist, 0, 0);
        // check if the planet has been moved
        if (planet != null)
        {
            planetMoved = true;
        }
        
    }
}
