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
        StartCoroutine(WaitForHabitableZone());
    }

    // Update is called once per frame
    void Update()
    {   
        if (outerHabitableZoneToShow == 0)
        {
            return;
        }

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
            float dist = planetZ / Mathf.Tan(30 * Mathf.Deg2Rad);
            dist = dist + planet.transform.localScale.x / 2;
            
            // Ensure the camera is always outside the habitable zone circle
            float minDistance = outerHabitableZoneToShow * 1.1f; // 10% buffer
            if (dist < minDistance)
            {
                dist = minDistance;
            }

            // set the object position
            transform.position = new Vector3(dist, dist, 0);
            transform.rotation = Quaternion.Euler(57, -90, 0);
        }
    }

    private IEnumerator WaitForHabitableZone()
    {
        yield return new WaitForSeconds(1);
        outerHabitableZoneToShow = HabitableZone.outerHabitableZone;
        GetObjectsPosition();
    }

    void GetObjectsPosition()
    {
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

        // If there are more than 6 planets, use the habitable zone distance
        if (planets.Length > 6)
        {
            planetPosition = new Vector3(outerHabitableZoneToShow, 0, 0);
        }
        else
        {
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
}
