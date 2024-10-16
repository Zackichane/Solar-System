using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    private float minSize;
    private float maxSize;
    private GameObject starPrefab;
    private GameObject generatedStar;
    private const float scale = 10000f;
    public GameObject WhiteDwarf;
    public GameObject YellowDwarf;
    public GameObject RedGiant;
    public GameObject BlueGiant;


    void Start()
    {
        int starType = PlayerPrefs.GetInt("starType");
        GetSizeAndDistance(starType);
        GenerateStar();
    }

    void Update()
    {
        
    }

    void GetSizeAndDistance(int starType)
    {
        if (starType == 1)
        {
            starPrefab = WhiteDwarf;
            // 7000 km to 14000 km
            minSize = 7000 / scale;
            maxSize = 14000 / scale;
        }
        else if (starType == 2)
        {
            starPrefab = YellowDwarf;
            // 1 120 000 km to 1 680 000 km
            minSize = 1120000 / scale;
            maxSize = 1680000 / scale;
        }
        else if (starType == 3)
        {
            starPrefab = RedGiant;
            // 99 779 000 km to 997 790 000 km
            minSize = 99779000 / scale;
            maxSize = 997790000 / scale;
        }
        else if (starType == 4)
        {
            starPrefab = BlueGiant;
            // 14 000 000 km to 140 000 000 km
            minSize = 14000000 / scale;
            maxSize = 140000000 / scale;
        }
    }

    void GenerateStar()
    {
        // Determine the size of the star based on the min and max size
        float starSize = Random.Range(minSize, maxSize);

        // Instantiate the star at position 0, 0, 0 with random size
        generatedStar = Instantiate(starPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        generatedStar.transform.localScale = new Vector3(starSize, starSize, starSize);

        // Rename the genereated star to "GeneratedStar"
        generatedStar.name = "GeneratedStar";
        generatedStar.tag = "GeneratedStar";
        print("the star was generated");
    }
}
