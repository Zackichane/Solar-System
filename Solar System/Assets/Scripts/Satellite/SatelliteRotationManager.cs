using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteRotationManager : MonoBehaviour
{
    // set the centerobject position to 0,0,0
    private Transform centerObject;
    private float rotationSpeed = 1f;
    private string number;
    private GameObject generatedPlanet;
    private GameObject star = null;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(1f, 20f);
        centerObject = GetComponent<planetTracker>().planet.transform;
        generatedPlanet = centerObject.gameObject; // Ensure generatedPlanet is assigned
        StartCoroutine(GetStarByName("GeneratedStar")); // Start the coroutine
    }

    // Update is called once per frame
    void Update()
    {
        OrbiteSatellite();
        RotateSatellite();
    }

    void OrbiteSatellite() 
    {   if (star == null)
        {
            return;
        }
        if (centerObject != null && generatedPlanet != null && Camera.main.name != "CAM Satellite")
        {
            transform.RotateAround(centerObject.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    void RotateSatellite()
    {
        // Implementation for rotating the satellite
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    IEnumerator GetStarByName(string starName)
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second
        star = GameObject.Find(starName); // Find the star by its name
    }
}