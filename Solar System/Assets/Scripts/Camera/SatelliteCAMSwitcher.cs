// SCRIPT FOR BUTTON TO CHANGE CAMERA TO PLANET + SATELLITE

using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SatelliteCAMSwitcher : MonoBehaviour
{
    public Canvas canvas;
    public GameObject button;
    private GameObject planet;
    private GameObject satellite;
    private GameObject star;
    private GameObject[] planets;
    private Camera[] camerasDraft;
    private ParticleSystem[] particles;
    private GameObject[] satellites;
    public Camera satelliteCam;
    private Camera[] cameras;
    private int currentIndex = 0;
    public Vector3 offset;    // Offset from the target
    public float smoothSpeed = 0.125f; // Smoothness factor for movement
    private Transform planetToTrack; // Change type to Transform
    private Transform satelliteToTrack; // Add this line

    void Start()
    {
        button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(SwitchCamera);
    }

    void Update()
    {
        if (star == null)
        {
            star = GameObject.Find("GeneratedStar");
            particles = star.GetComponentsInChildren<ParticleSystem>();
        }

        if (planetToTrack != null)
        {
            // Desired position
            Vector3 desiredPosition = planetToTrack.position + offset;
            // Smoothly interpolate to the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Smoothly rotate to look at the target
            Quaternion targetRotation = Quaternion.LookRotation(planetToTrack.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed);
        }
    }

    void SwitchCamera()
    {
        // desactiver toutes les cameras
        cameras = GameObject.FindObjectsOfType<Camera>();
        foreach (Camera c in cameras)
        {
            c.enabled = false;
        }
        // activer la camera satelliteCam
        satelliteCam.enabled = true;

        // obtenir une liste de toutes les planetes
        planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
        // mettre les planetes en ordre croissant par rapport a leur numero (GeneratedPlanet1, GeneratedPlanet2, etc.)
        planets = planets.OrderBy(p => p.name).ToArray();
        
        // obtenir le nombre de planetes
        int planetCount = planets.Length;

        // obtenir une liste de tous les satellites
        satellites = GameObject.FindGameObjectsWithTag("GeneratedSatellite");

        // obtenir la planete actuelle
        GameObject currentPlanet = planets[currentIndex % planetCount];
        // obtenir les satellites de la planete actuelle
        GameObject[] satellitesOfCurrentPlanet = satellites.Where(s => s.GetComponent<planetTracker>().planet == currentPlanet).ToArray();

        // cacher toutes les planetes sauf la planete actuelle
        foreach (GameObject p in planets)
        {
            if (p != currentPlanet)
            {
                Hide(p);
            }
        }
        // cacher tous les satellites
        foreach (GameObject s in satellites)
        {
            if (!satellitesOfCurrentPlanet.Contains(s))
            {
                Hide(s);
            }
        }
        // afficher la planete actuelle
        Show(currentPlanet);

        // obtenir le satellite actuel de la planete actuelle
        satelliteToTrack = satellitesOfCurrentPlanet[currentIndex / planetCount % satellitesOfCurrentPlanet.Length].transform;

        // afficher le satellite actuel
        Show(satelliteToTrack.gameObject);

        // desactiver l'etoile et ses particules
        Hide(star);
        particles[0].gameObject.SetActive(false);

        canvas.worldCamera = satelliteCam;

        // change the satelliteCam object follower to the current satellite
        satelliteCam.GetComponent<CamObjFollow>().targetName = satelliteToTrack.name;
        // ajouter le nom de la planete a suivre
        satelliteCam.GetComponent<CamObjFollow>().secondTargetNames = new List<string> { currentPlanet.name };

        currentIndex++;
        if (currentIndex >= planetCount)
        {
            currentIndex = 0;
        }
    }

    public void Hide(object objectToHide)
    {
        GameObject gameObjectToHide = objectToHide as GameObject;
        if (gameObjectToHide != null)
        {
            gameObjectToHide.GetComponent<MeshRenderer>().enabled = false;
        }

    }

    public void Show(object objectToShow)
    {
        GameObject gameObjectToShow = objectToShow as GameObject;
        if (gameObjectToShow != null)
        {
            gameObjectToShow.GetComponent<MeshRenderer>().enabled = true; // Ensure MeshRenderer is enabled
        }

    }

    public void HideOtherPlanets()
    {
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
