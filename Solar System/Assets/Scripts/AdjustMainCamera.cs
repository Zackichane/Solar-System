using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustMainCamera : MonoBehaviour
{
    private GameObject planet;
    private Vector3 planetPosition;
    private Vector3 starPosition;
    private float outerHabitableZoneToShow = 0;

    void Start()
    {
        GetObjectsPosition();
    }

    // Update is called once per frame
    void Update()
    {   
        if (outerHabitableZoneToShow == 0)
        {
            outerHabitableZoneToShow = PlayerPrefs.GetInt("outerHabitableZoneToShow");
            Debug.Log($"Habitable Zone To Show (Outer): {outerHabitableZoneToShow} Km/scale");
        }
        // if the planet position is 0, 0, 0 retry
        if (planetPosition == Vector3.zero & outerHabitableZoneToShow != 0)
        {
            GetObjectsPosition();
        }
        if (planet == null & outerHabitableZoneToShow != 0)
        {
            // if the planet or star is not found, try to find them again
            GetObjectsPosition();
        }
        if (planet != null)
        {
            float planetZ = planetPosition.x;
            // use tan to calculate the distance between the planet and the star
            float dist = (planetZ) / 0.5773502692f;
            dist = dist + planet.transform.localScale.x / 2;
            // set the object position
            transform.position = new Vector3(dist, dist, 0);
        }
    }

    void GetObjectsPosition()
    {
        // Assuming you have tags assigned to your planet and star objects
        //planet = GameObject.Find("GeneratedPlanet");

        // get all objects with the tag "GeneratedPlanet"
        GameObject[] planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
        // get the farthest planet
        float maxX = 0;
        foreach (GameObject p in planets)
        {
            if (p.transform.position.x > maxX)
            {
                planet = p;
                maxX = p.transform.position.x;
            }
        }

        

        if (outerHabitableZoneToShow >= planet.transform.position.x)
        {
            planetPosition = new Vector3(outerHabitableZoneToShow, 0, 0);
        }
        else
        {
            planetPosition = planet.transform.position;
        }
    }
}
