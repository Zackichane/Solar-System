using UnityEngine;
using static System.Math;


public class YellowDwarfGenerator : MonoBehaviour
{
    public GameObject YellowDwarf;  // Prefab of the star
    public GameObject WhiteDwarf;
    public GameObject RedGiant;
    public GameObject BlueGiant;
    public Camera mainCamera;
    private GameObject generatedStar;
    private GameObject starPrefab;

    // Minimum and maximum sizes in kilometers (scale : 1 unit = 10 000 km)
    private float minSizeKm;
    private float maxSizeKm;
    private const float scale = 10000f;
    public float rotationSpeed = 1f;

    private float minDist;
    private float maxDist;
    private float radiusStar;

    void Start()
    {
        
        // select random star type
        int starType = Random.Range(1, 5);
        if (starType == 1)
        {
            starPrefab = WhiteDwarf;
            // 7000 km to 14000 km
            minSizeKm = 7000/ scale;
            maxSizeKm = 14000/ scale;
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
    }

    void GenerateYellowDwarf()
    {
        // Generate a random size in kilometers and convert to Unity units
        float randomSizeKm = Random.Range(minSizeKm, maxSizeKm);
        float randomSizeUnity = randomSizeKm;

        radiusStar = randomSizeKm/2f;

        float randomDist = Random.Range(minDist, maxDist);

        // Instantiate the star (yellow dwarf) at a position (0, 0, 0) with random size
        generatedStar = Instantiate(starPrefab, Vector3.zero, Quaternion.identity);

        // Apply the size (localScale) based on the random size
        generatedStar.transform.localScale = new Vector3(randomSizeUnity, randomSizeUnity, randomSizeUnity);

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
        starLight.range = randomSizeUnity * 10f; // Adjust light range based on size


        // adjust camera position
        mainCamera.transform.position = new Vector3(randomSizeKm, 0, 0);
        float dist = 0f;

        // move the planet empty object
        GameObject planetEmpty = GameObject.Find("habitable(Clone)");
        float radiusPlanet = planetEmpty.transform.localScale.x / 2f;
        if (radiusPlanet > radiusStar)
        {
            float toAdd = radiusPlanet - radiusStar;
            planetEmpty.transform.position = new Vector3(0, planetEmpty.transform.position.x + toAdd, 0);
            dist = dist + radiusPlanet;
        }
        // random distance between 100 and 2000

        dist = dist + (randomSizeKm / 2f + randomDist + radiusStar);
        // round to 2 decimal places
        dist = (float)Round(dist * 100f) / 100f;
        planetEmpty.transform.position = new Vector3(dist, 0, 0);
    }
    void RotateYellowDwarf()
    {
        if (generatedStar != null)
        {
            // Rotate the star around its Y-axis
            generatedStar.transform.Rotate(Vector3.up, rotationSpeed);
        }
    }
}
