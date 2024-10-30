using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbiteHandler : MonoBehaviour
{
    public float orbiteNumber;
    public GameObject planet;
    public bool EarthLikePlanets;
    public bool GazGiantPlanets;
    public bool MarsLikePlanets;
    public bool MercuryLikePlanets;
    public bool VenusLikePlanets;


    private int nOrbits;
    void Start()
    {
        nOrbits = PlayerPrefs.GetInt("nPlanets");
        // print the number of orbits
        Debug.Log("Number of orbits: " + nOrbits);
        // get all the planets
        GameObject[] planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
        // get the star
        GameObject star = GameObject.FindGameObjectWithTag("GeneratedStar");
        // get the star size
        float starRadius = star.transform.localScale.x / 2;
        print("Star radius: " + starRadius);
        // the star is at (0,0,0)
        float minOrbiteDist = starRadius + 1;
        MovePlanet(planets, minOrbiteDist);
    }

    void Update()
    {
        
    }

    void MovePlanet(GameObject[] planets, float minOrbiteDist)
    {
        // get the main planet
        GameObject mainPlanet = planets[0];
        // set the main planet as the parent
        mainPlanet.transform.parent = transform;
        // set the main planet position
        mainPlanet.transform.position = new Vector3(minOrbiteDist, 0, 0);
    }

}
