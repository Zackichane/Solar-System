using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteGenerate : MonoBehaviour
{
    private GameObject satellitePrefab;
    private const float scale = 10000f;
    private GameObject generatedSatellite;
    public GameObject S1;
    public GameObject S2;
    public GameObject S3;
    public GameObject S4;
    public GameObject S5;
    public GameObject S6;
    public GameObject S7;
    public GameObject S8;
    public GameObject S9;
    public GameObject S10;
    public GameObject S11;
    public GameObject S12;
    public GameObject S13;
    public GameObject S14;
    public GameObject S15;
    public GameObject S16;
    public GameObject S17;
    public GameObject S18;
    public GameObject S19;
    public GameObject S20;
    public GameObject S21;
    public GameObject S22;
    public GameObject S23;
    public GameObject S24;
    public GameObject S25;
    public GameObject S26;
    public GameObject S27;
    public GameObject S28;
    public GameObject S29;
    public GameObject S30;
    public GameObject S31;
    public GameObject S32;
    public GameObject S33;
    public GameObject S34;
    public GameObject S35;
    public GameObject S36;
    public GameObject S37;
    public GameObject S38;
    public GameObject S39;
    public GameObject S40;
    private GameObject[] generatedPlanet;
    private Transform centerObject;
    public float orbiteSpeed = 100f;
    public float rotationSpeed = 50f;
    private string planetName = "GeneratedPlanet";
    public bool stopOrbite = false;
    public bool randomPrefab = true;

    // add a list of satellites s1 to s22
    public List<GameObject> satellites = new List<GameObject>();

    void Start()
    {
        // Add satellites to the list
        satellites.Add(S1);
        satellites.Add(S2);
        satellites.Add(S3);
        satellites.Add(S4);
        satellites.Add(S5);
        satellites.Add(S6);
        satellites.Add(S7);
        satellites.Add(S8);
        satellites.Add(S9);
        satellites.Add(S10);
        satellites.Add(S11);
        satellites.Add(S12);
        satellites.Add(S13);
        satellites.Add(S14);
        satellites.Add(S15);
        satellites.Add(S16);
        satellites.Add(S17);
        satellites.Add(S18);
        satellites.Add(S19);
        satellites.Add(S20);
        satellites.Add(S21);
        satellites.Add(S22);
        satellites.Add(S23);
        satellites.Add(S24);
        satellites.Add(S25);
        satellites.Add(S26);
        satellites.Add(S27);
        satellites.Add(S28);
        satellites.Add(S29);
        satellites.Add(S30);
        satellites.Add(S31);
        satellites.Add(S32);
        satellites.Add(S33);
        satellites.Add(S34);
        satellites.Add(S35);
        satellites.Add(S36);
        satellites.Add(S37);
        satellites.Add(S38);
        satellites.Add(S39);
        satellites.Add(S40);
    
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
        else
        {
            // Select a specific satellite prefab
            satellitePrefab = S1;
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
        satellitePrefab = S1;
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
