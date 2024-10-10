// SCRIPT FOR BUTTON TO CHANGE CAMERA TO PLANET + SATELLITE

using UnityEngine;
using System.Linq;

public class PlanetCAMSwitcher : MonoBehaviour
{
    public Canvas canvas;
    public GameObject button;
    private GameObject planet;
    private GameObject satellite;
    private GameObject star;
    private Camera currentCamera;
    private GameObject[] planets;
    private GameObject[] cameraObjects;
    private Camera[] cameras;
    private Camera[] camerasDraft;
    private Camera nextCamera;
    private ParticleSystem[] particles;
    private GameObject[] satellites;

    void Start()
    {
        button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(SwitchCamera);
    }



    void Update()
    {
        if (planets == null)
        {
            planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
        }
        if (satellite == null)
        {
            satellites = GameObject.FindGameObjectsWithTag("GeneratedSatellite");
        }
        if (star == null)
        {
            star = GameObject.Find("GeneratedStar");
            particles = star.GetComponentsInChildren<ParticleSystem>();
        }
        if (cameraObjects == null)
        {
            // get all the objects with the MainCamera tag
            cameraObjects = GameObject.FindGameObjectsWithTag("MainCamera");
            Debug.Log("Number of camera objects found: " + cameraObjects.Length);

            camerasDraft = new Camera[cameraObjects.Length];
            for (int i = 0; i < cameraObjects.Length; i++)
            {
                camerasDraft[i] = cameraObjects[i].GetComponent<Camera>();
                if (camerasDraft[i] != null)
                {
                    Debug.Log("Camera found: " + camerasDraft[i].name);
                }
                else
                {
                    Debug.LogWarning("No Camera component found on: " + cameraObjects[i].name);
                }
            }
        }

        for (int i = 0; i < camerasDraft.Length; i++)
        {
            if (camerasDraft[i] != null && camerasDraft[i].name.Contains("CameraGeneratedPlanet"))
            {
                // add camerasDraft[i] to the cameras array
                if (cameras == null)
                {
                    cameras = new Camera[1];
                    cameras[0] = camerasDraft[i];
                }
                else
                {
                    cameras = cameras.Concat(new Camera[] { camerasDraft[i] }).ToArray();
                }
            }
        }
    }

    void SwitchCamera()
    {
        currentCamera = Camera.main;
        // if the camera is a planet camera then switch to the next camera
        if (currentCamera.name.Contains("CameraGeneratedPlanet"))
        {
            // get the index of the current camera
            for (int i = 0; i < cameras.Length; i++)
            {
                if (cameras[i] == currentCamera)
                {
                    i++;
                    if (i >= cameras.Length)
                    {
                        i = 0; // Reset to the first camera if we reach the end of the array
                    }
                    nextCamera = cameras[i];
                    break;
                }
            }
            
        }
        // if the camera is not a planet camera then switch to the first planet camera
        else
        {
            int n = 0;
            nextCamera = cameras[0];
        }
        currentCamera.enabled = false;
        nextCamera.enabled = true;

        string cameraNumber = nextCamera.name.Substring(nextCamera.name.Length - 1);
        string planetName = "GeneratedPlanet" + cameraNumber;
        planet = GameObject.Find(planetName);
        planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");

        string satelliteName = "GeneratedSatellite" + cameraNumber;
        satellite = GameObject.Find(satelliteName);

        HideOtherPlanets(nextCamera.name);
        Show(star);
        particles[0].gameObject.SetActive(false);
        Show(satellite);
        HideOtherSatellites(satellite);
        Show(planet);
        canvas.worldCamera = nextCamera;
    }

    public void Hide(object objectToHide)
    {
        GameObject gameObjectToHide = objectToHide as GameObject;
        if (gameObjectToHide != null)
        {
            gameObjectToHide.GetComponent<MeshRenderer>().enabled = false;
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
            gameObjectToShow.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            Debug.LogWarning("The object to show is not a GameObject!");
        }
    }

    public void HideOtherPlanets(string nextCameraName)
    {
        string cameraNumber = nextCameraName.Substring(nextCameraName.Length - 1);
        string planetName = "GeneratedPlanet" + cameraNumber;
        planet = GameObject.Find(planetName);
        planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
        foreach (GameObject p in planets)
        {
            if (p != planet)
            {
                Hide(p);
            }
        }
    }

    public void HideOtherSatellites(GameObject satellite)
    {
        satellites = GameObject.FindGameObjectsWithTag("GeneratedSatellite");
        foreach (GameObject s in satellites)
        {
            if (s != satellite)
            {
                Hide(s);
            }
        }
    }
}