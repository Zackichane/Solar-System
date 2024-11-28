using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotationManager : MonoBehaviour
{
    // set the centerobject position to 0,0,0
    private Vector3 centerCoordonates = new Vector3(0, 0, 0);
    private float rotationSpeed = 0f;
    private bool stopOrbite = false;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(5f, 25f);
    }

    // Update is called once per frame
    void Update()
    {
        // if the satellite is inactive, stop the planet orbit
        GameObject generatedStar = GameObject.Find("GeneratedStar");
        // stop the orbite only when on the planet camera
        if (Camera.main.name == "CAM Planet")
        {
            stopOrbite = true;
        }
        else
        {
            stopOrbite = false;
        }

        if (stopOrbite == false)
        {
            RotatePlanet();
        }

        RotatePlanetOnAxis();
    }

    void RotatePlanet()
    {
        transform.RotateAround(centerCoordonates, Vector3.up, rotationSpeed * Time.deltaTime);
    }

    // rotate the planet on its own axis
    void RotatePlanetOnAxis()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
