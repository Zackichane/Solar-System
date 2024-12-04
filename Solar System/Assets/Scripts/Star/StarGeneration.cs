using UnityEngine;
using static System.Math;

public class StarGeneration : MonoBehaviour
{
    public GameObject YellowDwarf;  // Prefab of the star
    public GameObject WhiteDwarf;
    public GameObject RedGiant;
    public GameObject BlueGiant;
    public GameObject SiriusB;
    public GameObject VanMaanen;
    public GameObject ProximaCentauri;
    public GameObject Wolf359;
    public GameObject Gilese581;
    public GameObject TauCeti;
    public GameObject TheSun;
    public GameObject AlphaCentauri;
    public GameObject Ton618;
    public Camera mainCamera;
    private GameObject generatedStar;
    private GameObject starPrefab;
    private GameObject[] listOfPlanets;

    // Minimum and maximum sizes in kilometers (scale : 1 unit = 10 000 km)
    private float minSizeKm;
    private float maxSizeKm;
    public static float starSize { get; private set; }
    private const float scale = 10000f;
    public float rotationSpeed = 1f;
    private float minTemp;
    private float maxTemp;
    public static float starTemperature { get; private set; }
    private float randomSizeKm;

    private float starLuminosity;
    public static float Luminosity;
    public static float LuminosityToShow;
    

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
            minTemp = 5000;
            maxTemp = 100000;
        }
        else if (starType == 2)
        {
            starPrefab = YellowDwarf;
            // 1 120 000 km to 1 680 000 km
            minSizeKm = 1120000 / scale;
            maxSizeKm = 1680000 / scale;
            minTemp = 5000;
            maxTemp = 6000;

        }
        else if (starType == 3)
        {
            starPrefab = RedGiant; // maintenant naine rouge
            // 99 779 000 km to 997 790 000 km
            minSizeKm = 70000 / scale;
            maxSizeKm = 500000 / scale;
            maxTemp = 2500;
            minTemp = 6000;
        }
        else if (starType == 4)
        {
            starPrefab = BlueGiant; // maintenant naine bleue
            // 14 000 000 km to 140 000 000 km
            minSizeKm = 50000 / scale;
            maxSizeKm = 400000 / scale;
            minTemp = 10000;
            maxTemp = 30000;

        }
        else if (starType == 5)
        {
            starPrefab = SiriusB;
            starSize = 12000 / scale;
            starTemperature = 25200;
        }
        else if (starType == 6)
        {
            starPrefab = VanMaanen;
            starSize = 15300 / scale;
            starTemperature = 6200;
        }
        else if (starType == 7)
        {
            starPrefab = ProximaCentauri;
            starSize = 214000 / scale;
            starTemperature = 3050;
        }
        else if (starType == 8)
        {
            starPrefab = Wolf359;
            starSize = 222620 / scale;
            starTemperature = 2800;
        }
        else if (starType == 9)
        {
            starPrefab = Gilese581;
            starSize = 416000 / scale;
            starTemperature = 3400;
        }
        else if (starType == 10)
        {
            starPrefab = TauCeti;
            starSize = 1100000 / scale;
            starTemperature = 5350;
        }
        else if (starType == 11)
        {
            starPrefab = TheSun;
            starSize = 1390000 / scale;
            starTemperature = 5770;
        }
        else if (starType == 12)
        {
            starPrefab = AlphaCentauri;
            starSize = 1700000 / scale;
            starTemperature = 5260;
        }
        else
        {
            starPrefab = Ton618;
            starSize = 390000000000;
            starTemperature = 0;
        }
        GenerateYellowDwarf();


    }

    void Update()
    {
        RotateYellowDwarf();
        if (generatedStar.name != "GeneratedStar") { generatedStar.name = "GeneratedStar"; }
    }

    void GenerateYellowDwarf()
    {
        randomSizeKm = Random.Range(minSizeKm, maxSizeKm);

        starSize = randomSizeKm;

        // Instantiate the star (yellow dwarf) at a position (0, 0, 0) with random size
        generatedStar = Instantiate(starPrefab, Vector3.zero, Quaternion.identity);

        // Apply the size (localScale) based on the random size
        generatedStar.transform.localScale = new Vector3(randomSizeKm, randomSizeKm, randomSizeKm);

        starTemperature = Random.Range(minTemp, maxTemp);

        // Calculate luminosity in watts
        starLuminosity = 4f * Mathf.PI * Mathf.Pow(starSize * 10000000f / 2f, 2f) * (float)(5.670374419e-8) * Mathf.Pow(starTemperature, 4f);
        LuminosityToShow = starLuminosity;

        // Convert the luminosity to solar units
        float solarLuminosity = starLuminosity / 3.828e26f;  // Sun's luminosity is 3.828 x 10^26 W
        Luminosity = solarLuminosity;
        
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
}
