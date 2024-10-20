// SCRIPT FOR BUTTON TO CHANGE CAMERA TO PLANET + SATELLITE

using UnityEngine;
using System.Linq;

public class BTN_CAM_Switcher : MonoBehaviour
{

    public Camera cameraToSwitch;
    public bool forPlanet = false;
    public bool forSatellite = false;
    public bool forStar = false;
    public Canvas canvas;
    public GameObject button;
    private GameObject planet;
    private GameObject satellite;
    private GameObject star;
    private Camera currentCamera;
    private GameObject[] planets;
    private ParticleSystem[] particles;
    

    void Start()
    {
        button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(SwitchCamera);
    }



    void Update()
    {
        if (planets == null)
        {
            // get all the objects with the tag "GeneratedPlanet"
            planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
        }
        if (satellite == null)
        {
            satellite = GameObject.Find("GeneratedSatellite");
        }
        if (star == null)
        {
            star = GameObject.Find("GeneratedStar");
            // get the the chlildren of the star that is particles
            particles = star.GetComponentsInChildren<ParticleSystem>();
        }
    }

    void SwitchCamera()
    {
        currentCamera = Camera.main;
        currentCamera.enabled = false;
        cameraToSwitch.enabled = true;

        if (forSatellite)
        {
            HideOtherPlanets();
            Hide(star);
            particles[0].gameObject.SetActive(false);
            Show(satellite);
        }
        else if (forStar)
        {
            HideOtherPlanets();
            Show(satellite);
            Show(star);
            particles[0].gameObject.SetActive(true);
        }
        canvas.worldCamera = cameraToSwitch;
    }

    public void Hide(object objectToHide)
    {
        GameObject gameObjectToHide = objectToHide as GameObject;
        if (gameObjectToHide != null)
        {
            //gameObjectToHide.SetActive(false);
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
            //gameObjectToShow.SetActive(true);
            gameObjectToShow.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            Debug.LogWarning("The object to show is not a GameObject!");
        }
    }

    public void HideOtherPlanets()
    {
        planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
        // check the game objects, if it's not the planet, hide it
        foreach (GameObject p in planets)
        {
            Show(p);
        }
    }
}
