using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteGenerate : MonoBehaviour
{
    private GameObject satellitePrefab;
    private const float scale = 10000f;
    private GameObject generatedSatellite;
    private GameObject[] generatedPlanet;
    private Transform centerObject;
    public float orbiteSpeed = 100f;
    public float rotationSpeed = 50f;
    private string planetName = "GeneratedPlanet";
    public bool stopOrbite = false;
    public bool randomPrefab = false;

    // add a list of satellites s1 to s22
    public List<GameObject> satellites = new List<GameObject>();

    void Start()
    {
            
    }

    void Update()
    {
        // check if the planet was generated
        if (generatedPlanet == null)
        {
            generatedPlanet = GameObject.FindGameObjectsWithTag("GeneratedPlanet");

            if (generatedPlanet != null && generatedPlanet.Length > 0)
            {
                // generate a satellite for each planet
                for (int i = 0; i < generatedPlanet.Length; i++)
                {
                    centerObject = generatedPlanet[i].transform;
                    int minSatellites = 1;
                    int maxSatellites = 5;
                    int numSatellites = Random.Range(minSatellites, maxSatellites + 1);
                    for (int j = 0; j < numSatellites; j++)
                    {
                        GenerateSatellite(centerObject.gameObject);
                    }
                }
            }
        }


        //GameObject planetObject = GameObject.Find("GeneratedPlanet");
        //if (planetObject == null)
        //{
        //    stopOrbite = true;
        //}
        //else
        //{
        //    stopOrbite = false;
        //}
//
        //if (stopOrbite == false)
        //{
        //    OrbiteSatellite();
        //}
        //RotateSatellite();
    }

    void GenerateSatellite(GameObject planetObject)
    {
        if (randomPrefab)
        {
            // Randomly select a satellite prefab
            int index = Random.Range(0, satellites.Count);
            if (satellites[index] != null)
            {
                satellitePrefab = satellites[index];
            }
        }
        if (satellitePrefab == null)
        {
            Debug.LogError("Satellite prefab not found!");
            return;
        }

        // Calculate the size and spawn position of the satellite
        float sizePlanet = planetObject.transform.localScale.x;
        float minSize = 10000/scale;
        float maxSize = 15868/scale;
        float sizeSatellite = Random.Range(minSize, maxSize);

        // Set the scale of the satellite
        float distance = sizePlanet * 2f; // Set the desired distance from the planet based on the planet size
        Vector3 spawnPosition = planetObject.transform.position + new Vector3(distance, 0, 0); // Adjust spawn position relative to the planet

        // Instantiate the satellite
        generatedSatellite = Instantiate(satellitePrefab, spawnPosition, Quaternion.identity);
        generatedSatellite.transform.localScale = new Vector3(sizeSatellite, sizeSatellite, sizeSatellite);

        // Rename the satellite
        generatedSatellite.name = "GeneratedSatellite" + planetObject.name.Substring(15);
        // add a tag
        generatedSatellite.tag = "GeneratedSatellite";
        generatedSatellite.AddComponent<SatelliteRotationManager>();

        // generate a camera for the satellite
        GameObject camera = new GameObject();
        camera.AddComponent<Camera>();
        camera.AddComponent<CamObjFollow>();
        camera.GetComponent<CamObjFollow>().targetName = generatedSatellite.name;
        camera.GetComponent<CamObjFollow>().secondTargetName = planetObject.name;
        camera.name = "Camera" + generatedSatellite.name;
        camera.tag = "MainCamera";
        camera.GetComponent<Camera>().enabled = false;
    }

    
}
