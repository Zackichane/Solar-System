using UnityEngine;

public class PlanetGenerator : MonoBehaviour

public Vector3 spawnposition = new Vector3(10, 10, 10)

{
    public GameObject planetPrefab;  // Prefab of the star
    private GameObject generatedPlanet;

    // Minimum and maximum sizes in kilometers (scaled to Unity units)
    private const float minSizeKm = 0.3f; // 1.253 million km
    private const float maxSizeKm = 1f; // 1.671 million km
    public float rotationSpeed = 1f;

    void Start()
    {
        GeneratePlanet();
    }

    void Update()
    {
        RotatePlanet();
    }

    void GeneratePlanet()
    {
        // Generate a random size in kilometers and convert to Unity units
        float randomSizeKm = Random.Range(minSizeKm, maxSizeKm);
        float randomSizeUnity = randomSizeKm;

        // Instantiate the star (yellow dwarf) at a position (0, 0, 0) with random size
        generatedPlanet = Instantiate(planetPrefab, spawnposition, Quaternion.identity);

        // Apply the size (localScale) based on the random size
        generatedPlanet.transform.localScale = Vector3(randomSizeUnity, randomSizeUnity, randomSizeUnity);
    
    }


    void RotatePlanet()
    {
        if (generatedPlanet != null)
        {
            // Rotate the star around its Y-axis
            generatedPlanet.transform.Rotate(Vector3.up, rotationSpeed);
        }
    }
}
