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

    public int starSize;

    void Start()
    {
        // get the starType in the playerprefs that is an int
        int starType = PlayerPrefs.GetInt("starType");

        if (starType == 1 || starType == 5 || starType == 6)
        {
            starPrefab = WhiteDwarf;
        }
        else if (starType == 2 || starType == 10 || starType == 11 || starType == 12)
        {
            starPrefab = YellowDwarf;
        }
        else if (starType == 3 || starType == 7 || starType == 8 || starType == 9)
        {
            starPrefab = RedGiant;
        }
        else if (starType == 4)
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
