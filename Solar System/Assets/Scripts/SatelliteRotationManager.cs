using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteRotationManager : MonoBehaviour
{
    // set the centerobject position to 0,0,0
    private Transform centerObject;
    private float rotationSpeed = 0f;
    private bool stopOrbite = false;
    private string name;
    private string number;
    private GameObject generatedPlanet;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(1f, 20f);
        // get the name of the GameObject with the script
        name = gameObject.name;
        // get the number at the end of the name GeneratedSatellite1
        number = name.Substring(name.Length - 1);
        // get the generated planet
        generatedPlanet = GameObject.Find("GeneratedPlanet" + number);
        centerObject = generatedPlanet.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // if the satellite is inactive, stop the planet orbit
        GameObject generatedStar = GameObject.Find("GeneratedPlanet" + number);
        if (generatedStar.GetComponent<MeshRenderer>().enabled == false)
        {
            stopOrbite = true;
        }
        else
        {
            stopOrbite = false;
        }

        if (stopOrbite == false)
        {
            OrbiteSatellite();
        }

        RotateSatellite();
    }

    void OrbiteSatellite()
    {
        if (centerObject != null)
        {
            // Ensure the satellite is always at a fixed distance from the planet
            float distance = transform.localScale.x + generatedPlanet.transform.localScale.x; // Set the desired distance from the planet
            Vector3 direction = (transform.position - generatedPlanet.transform.position).normalized;
            transform.position = generatedPlanet.transform.position + direction * distance;

            // Rotate around the Y axis
            transform.RotateAround(centerObject.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    void RotateSatellite()
    {
        // Implementation for rotating the satellite
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
