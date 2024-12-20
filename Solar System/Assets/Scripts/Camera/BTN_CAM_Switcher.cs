// SCRIPT FOR BUTTON TO CHANGE CAMERA TO PLANET + SATELLITE

using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


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

        // start coroutine of 2s to wait for the objects to be generated
        StartCoroutine(WaitForObjects());
    }

    void Update()
    {

    }

    IEnumerator WaitForObjects()
    {
        yield return new WaitForSeconds(2);
        planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
        satellite = GameObject.Find("GeneratedSatellite");
        star = GameObject.Find("GeneratedStar");
        particles = star.GetComponentsInChildren<ParticleSystem>();
    }

    void SwitchCamera()
    {
        currentCamera = Camera.main;
        currentCamera.enabled = false;
        cameraToSwitch.enabled = true;

        if (forSatellite)
        {
            HideOtherPlanets();
            particles[0].gameObject.SetActive(false);
            Show(satellite);
            Show(star);
        }
        else if (forStar)
        {
            Show(satellite);
            Show(star);
            for (int i = 0; i < planets.Length; i++)
            {
                Show(planets[i]);
            }
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

    }

    public void Show(object objectToShow)
    {
        GameObject gameObjectToShow = objectToShow as GameObject;
        if (gameObjectToShow != null)
        {
            //gameObjectToShow.SetActive(true);
            gameObjectToShow.GetComponent<MeshRenderer>().enabled = true;
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
