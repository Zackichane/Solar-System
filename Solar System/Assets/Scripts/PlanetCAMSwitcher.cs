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
    private GameObject[] planets;
    private Camera[] camerasDraft;
    private ParticleSystem[] particles;
    private GameObject[] satellites;
    public Camera planetCam;
    private Camera[] cameras;
    private int currentIndex = 0;
    public Vector3 offset;    // Offset from the target
    public float smoothSpeed = 0.125f; // Smoothness factor for movement
    private Transform planetToTrack; // Change type to Transform

    void Start()
    {
        button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(SwitchCamera);
    }

    void Update()
    {
        // Remove the redundant listener assignment
        // button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(SwitchCamera);

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
        // activer la camera planetCam
        planetCam.enabled = true;

        // obtenir une liste de toutes les planetes
        planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
        // mettre les planetes en ordre croissant par rapport a leur numero (GeneratedPlanet1, GeneratedPlanet2, etc.)
        planets = planets.OrderBy(p => p.name).ToArray();
        
        // obtenir le nombre de planetes
        int planetCount = planets.Length;

        // obtenir une liste de tous les satellites
        satellites = GameObject.FindGameObjectsWithTag("GeneratedSatellite");

        planetToTrack = planets[currentIndex].transform; // Ensure planetToTrack is assigned correctly
        GameObject[] satellitesToTrack = satellites.Where(s => s.GetComponent<planetTracker>().planet == planetToTrack.gameObject).ToArray();
        
        // cacher les autres planetes
        foreach (GameObject p in planets)
        {
            if (p != planetToTrack.gameObject)
            {
                Hide(p);
            }
        }
        // cacher les autres satellites
        foreach (GameObject s in satellites)
        {
            if (!satellitesToTrack.Contains(s))
            {
                Hide(s);
            }
        }
        // afficher la planete
        Show(planetToTrack.gameObject); // Ensure the planet is shown
        // afficher les satellites
        foreach (GameObject s in satellitesToTrack)
        {
            Show(s);
        }

        // desactiver l'etoile et ses particules
        Hide(star);
        particles[0].gameObject.SetActive(false);

        canvas.worldCamera = planetCam;


        // change the planetCam object follower to the current planet
        planetCam.GetComponent<CamObjFollow>().targetName = planetToTrack.name;
        // ajouter tous les noms des satellites a suivre
        planetCam.GetComponent<CamObjFollow>().secondTargetNames = satellitesToTrack.Select(s => s.name).ToList();

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
