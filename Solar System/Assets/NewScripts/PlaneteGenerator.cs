using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;


public class PlaneteGenerator : MonoBehaviour
{
    private const float scale = 10000f;
    private const float minRocheuse = 22578 / scale;
    private const float maxRocheuse = 15868 / scale;
    private const float minGazeuse = 44254 / scale;
    private const float maxGazeuse = 482386 / scale;
    private float distanceFromStar;
    private float randomDist;
    private GameObject generatedPlanet;
    private int starType;
    public GameObject[] EarthLikePlanets;
    public GameObject[] GazGiantPlanets;
    public GameObject[] MarsLikePlanets;
    public GameObject[] MercuryLikePlanets;
    public GameObject[] VenusLikePlanets;
    void Start()
    {
        starType = PlayerPrefs.GetInt("starType");
        int nPlanets = 1;
        // set as playerprefs
        PlayerPrefs.SetInt("nPlanets", nPlanets);
        for (int i = 0; i < nPlanets; i++)
        {
            GeneratePlanet();
            if (GameObject.FindGameObjectsWithTag("GeneratedStar").Length == 1)
            {
                GetDistanceFromStar();
            }
        }
    }

    void Update()
    {
        
    }

    void GeneratePlanet()
    {
        // TODO:depending on the distance from the star, the planet will have a different type

        // Determine the distance from the star depending on the star radius and type   

        // TEMP : generate a rocky planet
        int t = 1;
        float randomSize = 0f;
        GameObject[] planetList = null;
        if (t == 1) // replace this with a future condition
        {
            planetList = EarthLikePlanets;
            randomSize = Random.Range(minRocheuse, maxRocheuse);
        }
        else if (t == 2)
        {
            planetList = MarsLikePlanets;
            randomSize = Random.Range(minRocheuse, maxRocheuse);
        }
        else if (t == 3)
        {
            planetList = VenusLikePlanets;
            randomSize = Random.Range(minRocheuse, maxRocheuse);
        }
        else if (t == 4)
        {
            planetList = GazGiantPlanets;
            randomSize = Random.Range(minRocheuse, maxRocheuse);
        }
        else if (t == 5)
        {
            planetList = MercuryLikePlanets;
            randomSize = Random.Range(minRocheuse, maxRocheuse);
        }

        GameObject planetPrefab = planetList[Random.Range(0, planetList.Length)];
        generatedPlanet = Instantiate(planetPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        generatedPlanet.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

        //MovePlanet();

        // rename the planet
        generatedPlanet.name = "GeneratedPlanet";
        // add the tag to the planet
        generatedPlanet.tag = "GeneratedPlanet";

        // add the component OrbiteHandler to the OrbiteHandler object 
        //GameObject orbiteHandlerObject = GameObject.Find("OrbiteHandler");
        //orbiteHandlerObject.AddComponent<OrbiteHandlerScript>();
        //orbiteHandler.planet = generatedPlanet;
        //OrbiteHandlerScript orbiteHandler = orbiteHandlerObject.GetComponent<OrbiteHandlerScript>();
        //orbiteHandler.orbiteNumber = 1;
        //if (planetList == EarthLikePlanets)
        //{
        //    orbiteHandler.EarthLikePlanets = true;
        //}
        //else if (planetList == MarsLikePlanets)
        //{
        //    orbiteHandler.MarsLikePlanets = true;
        //}
        //else if (planetList == VenusLikePlanets)
        //{
        //    orbiteHandler.VenusLikePlanets = true;
        //}
        //else if (planetList == GazGiantPlanets)
        //{
        //    orbiteHandler.GazGiantPlanets = true;
        //}
        //else if (planetList == MercuryLikePlanets)
        //{
        //    orbiteHandler.MercuryLikePlanets = true;
        //}
    }

    void GetDistanceFromStar()
    {
        if (starType == 1)
        {
            randomDist = Random.Range(10, 50);
        }
        else if (starType == 2)
        {
            randomDist = Random.Range(50, 100);
        }
        else if (starType == 3)
        {
            randomDist = Random.Range(1000, 3000);
        }
        else
        {
            randomDist = Random.Range(10000, 20000);
        }
    }

    void MovePlanet()
    {
        float dist = 0f;
        GameObject star = GameObject.FindGameObjectsWithTag("GeneratedStar")[0];
        float radiusStar = star.transform.localScale.x / 2f;


        float radiusPlanet = generatedPlanet.transform.localScale.x / 2f;
        if (radiusPlanet > radiusStar)
        {
            float toAdd = radiusPlanet + radiusStar;
            dist = toAdd;
        }
        else
        {
            dist = radiusStar;
        }

        dist = dist + (radiusStar + randomDist);
        // round to 2 decimal places
        dist = (float)Round(dist * 100f) / 100f;
        generatedPlanet.transform.position = new Vector3(dist, 0, 0);
    }
}
