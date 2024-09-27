using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
    public Camera camera4;
    public Camera camera5;

    public GameObject objectToSpawn;  // Object to spawn when C is pressed
    public float spawnDistance = 2f;  // Distance at which object spawns in front of the active camera

    private bool isCamera1Active = true;  // Track which camera is active

    private GameObject planet;
    private GameObject satellite;
    private GameObject star;
    public bool stopPlanetOrbite = false;
    public bool stopSatelliteOrbite = false;

    void Start()
    {
        // Ensure only the first camera is active at the start
        camera1.enabled = true; // main camera
        camera2.enabled = false; // planet camera
        camera3.enabled = false; // satellite camera
        camera4.enabled = false; // star camera
        camera5.enabled = false; // planet + satellite camera

        // Ensure camera movement control for camera1 is active at the start
        camera1.GetComponent<CameraController>().enabled = true;
        camera2.GetComponent<CameraController>().enabled = false;
        camera3.GetComponent<CameraController>().enabled = false;
        camera4.GetComponent<CameraController>().enabled = false;
        camera5.GetComponent<CameraController>().enabled = false;
    }

    void Update()
    {
        if (planet == null)
        {
            planet = GameObject.Find("GeneratedPlanet");
        }
        if (satellite == null)
        {
            satellite = GameObject.Find("GeneratedSatellite");
        }
        if (star == null)
        {
            star = GameObject.Find("GeneratedStar");
        }
        // Switch cameras when R is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Enable the active camera, disable the other one

            if (camera1.enabled == true)
            {
                camera1.enabled = false;
                camera2.enabled = true;
                Hide(star);
                Hide(satellite);
                Show(planet);
            }
            else if (camera2.enabled == true)
            {
                camera2.enabled = false;
                camera3.enabled = true;
                Hide(planet);
                Hide(star);
                Show(satellite);
            }
            else if (camera3.enabled == true)
            {
                camera3.enabled = false;
                camera4.enabled = true;
                Hide(planet);
                Hide(satellite);
                Show(star);
            }
            else if (camera4.enabled == true)
            {
                camera4.enabled = false;
                camera5.enabled = true;
                Hide(star);
                Show(satellite);
                Show(planet);
            }
            else if (camera5.enabled == true)
            {
                camera5.enabled = false;
                camera1.enabled = true;
                Show(star);
                Show(satellite);
                Show(planet);
            }

        }

        // Spawn object only with the active camera when C is pressed
        if (Application.isPlaying && Input.GetKeyDown(KeyCode.C))
        {
            if (isCamera1Active)
            {
                // Spawn object in front of camera1
                SpawnObject(camera1);
            }
            else
            {
                // Spawn object in front of camera2
                SpawnObject(camera2);
            }
        }
    }

    void SpawnObject(Camera activeCamera)
    {
        // Ensure objectToSpawn is assigned
        if (objectToSpawn != null)
        {
            // Calculate the spawn position in front of the active camera
            Vector3 spawnPosition = activeCamera.transform.position + activeCamera.transform.forward * spawnDistance;
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No object assigned to 'objectToSpawn'!");
        }
    }

    public void Hide(object objectToHide)
    {
        GameObject gameObjectToHide = objectToHide as GameObject;
        if (gameObjectToHide != null)
        {
            gameObjectToHide.SetActive(false);
        }
        else
        {
            Debug.LogWarning("The object to hide is not a GameObject!");
        }
    }

    public void Show(object objectToShow)
    {
        GameObject gameObjectToShow = objectToShow as GameObject;
        if (gameObjectToShow != null)
        {
            gameObjectToShow.SetActive(true);
        }
        else
        {
            Debug.LogWarning("The object to show is not a GameObject!");
        }
    }
}
