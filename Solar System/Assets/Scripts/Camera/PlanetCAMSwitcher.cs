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
    public float smoothSpeed = 0.125f; // Smoothness factor for movement
    private Transform planetToTrack; // Change type to Transform
    private Vector3 offset;


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
    // Deactivate all cameras except the planetCam
    cameras = GameObject.FindObjectsOfType<Camera>();
    foreach (Camera c in cameras)
    {
        c.enabled = false;
    }
    planetCam.enabled = true; // Make sure the planet camera is enabled

    // Get a list of all planets and sort them
    planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
    planets = planets.OrderBy(p => p.name).ToArray();

    int planetCount = planets.Length;

    // Get a list of all satellites
    satellites = GameObject.FindGameObjectsWithTag("GeneratedSatellite");

    // Set the planet to track
    planetToTrack = planets[currentIndex].transform;
    GameObject[] satellitesToTrack = satellites.Where(s => s.GetComponent<planetTracker>().planet == planetToTrack.gameObject).ToArray();

    // Hide other planets
    foreach (GameObject p in planets)
    {
        if (p != planetToTrack.gameObject)
        {
            Hide(p);
        }
    }

    // Hide other satellites
    foreach (GameObject s in satellites)
    {
        if (!satellitesToTrack.Contains(s))
        {
            Hide(s);
        }
    }

    // Show the selected planet
    Show(planetToTrack.gameObject);
    // Show the associated satellites
    foreach (GameObject s in satellitesToTrack)
    {
        Show(s);
    }

    // Deactivate the star and its particles
    Hide(star);
    particles[0].gameObject.SetActive(false);

    // Deactivate redSpheres specifically without affecting UI
    foreach (GameObject redSphere in GameObject.FindGameObjectsWithTag("RedSphere"))
    {
        if (redSphere != null)
        {
            redSphere.SetActive(false);
        }
    }

    // Make sure UI Canvas is still active and attached to the correct camera
    canvas.worldCamera = planetCam;

    // Update the planetCam's follower targets
    planetCam.GetComponent<CamObjFollow>().targetName = planetToTrack.name;
    planetCam.GetComponent<CamObjFollow>().secondTargetNames = satellitesToTrack.Select(s => s.name).ToList();

    // Update the current planet index and wrap around if necessary
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
