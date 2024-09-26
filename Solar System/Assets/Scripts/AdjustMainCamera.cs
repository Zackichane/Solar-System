using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustMainCamera : MonoBehaviour
{
    private GameObject planet;
    private GameObject star;
    private Vector3 planetPosition;
    private Vector3 starPosition;
    private bool cameraAdjusted = false;
    void Start()
    {
        // try to get the planet and star position
        GetObjectsPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (planet == null || star == null)
        {
            // if the planet or star is not found, try to find them again
            GetObjectsPosition();
            print(planetPosition);
            print(starPosition);
        }
        if (planet != null && star != null && !cameraAdjusted)
        {
            float planetZ = planetPosition.x;
            print(planetZ);
            // use tan to calculate the distance between the planet and the star
            float dist = (planetZ) / 0.5773502692f;
            // set the object position
            transform.position = new Vector3(0, 0, -dist);
            cameraAdjusted = true;
        }

    }

    void GetObjectsPosition()
    {
        planet = GameObject.Find("GeneratedPlanet");
        star = GameObject.Find("GeneratedStar");
        if (planet != null)
        {
            planetPosition = planet.transform.position;
        }
        if (star != null)
        {
            starPosition = star.transform.position;
        }
    }
}
