using UnityEngine;
using static System.Math;


public class StarGenerationMenu3DP : MonoBehaviour
{
    public GameObject YellowDwarf;  // Prefab of the star
    public GameObject WhiteDwarf;
    public GameObject RedGiant;
    public GameObject BlueGiant;
    private GameObject generatedStar;
    private GameObject starPrefab;

    public float rotationSpeed = 1f;
    public int x = 0;
    public int y = 13;
    public int z = 17;

    private float starSize;

    void Start()
    {
        // get the starType in the playerprefs that is an int
        int starType = PlayerPrefs.GetInt("starType");

        if (starType == 1)
        {
            starPrefab = WhiteDwarf;
        }
        else if (starType == 2)
        {
            starPrefab = YellowDwarf;
        }
        else if (starType == 3)
        {
            starPrefab = RedGiant;
        }
        else
        {
            starPrefab = BlueGiant;
        }
        GenerateYellowDwarf();
    }

    void Update()
    {
        RotateYellowDwarf();
    }

    void GenerateYellowDwarf()
    {
        int starSize = 5;

        // Instantiate the star (yellow dwarf) at a position (0, 0, 0) with random size
        
        generatedStar = Instantiate(starPrefab, new Vector3(x, y, z), Quaternion.identity);

        // Apply the size (localScale) based on the random size
        generatedStar.transform.localScale = new Vector3(starSize, starSize, starSize);

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
        starLight.range = starSize * 10f; // Adjust light range based on size

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
