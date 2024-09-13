using UnityEngine;

public class YellowDwarfGenerator : MonoBehaviour
{
    public GameObject yellow;  // Prefab of the star
    public GameObject white;
    public GameObject red;
    public GameObject blue;
    private GameObject generatedStar;
    private GameObject starPrefab;

    // Minimum and maximum sizes in kilometers (scaled to Unity units)
    private const float minSizeKm = 5f; // 1.253 million km
    private const float maxSizeKm = 40f; // 1.671 million km
    public float rotationSpeed = 1f;

    void Start()
    {
        
        // select random star type
        int starType = Random.Range(1, 5);
        if (starType == 1)
        {
            starPrefab = yellow;
        }
        else if (starType == 2)
        {
            starPrefab = white;
        }
        else if (starType == 3)
        {
            starPrefab = red;
        }
        else
        {
            starPrefab = blue;
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
