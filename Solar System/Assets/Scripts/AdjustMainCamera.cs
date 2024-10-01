using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustMainCamera : MonoBehaviour
{
    private GameObject planet;
    private Vector3 planetPosition;
    private Vector3 starPosition;
    void Start()
    {
        GetObjectsPosition();
    }

    // Update is called once per frame
    void Update()
    {    
        // if the planet position is 0, 0, 0 retry
        if (planetPosition == Vector3.zero)
        {
            GetObjectsPosition();
        }
        if (planet == null)
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
            transform.position = new Vector3(0, 0, -dist);
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


        if (planet != null)
        {
            planetPosition = planet.transform.position;
        }
    }
}
