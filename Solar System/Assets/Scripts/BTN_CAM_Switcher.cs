// SCRIPT FOR BUTTON TO CHANGE CAMERA TO PLANET + SATELLITE

using UnityEngine;

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

    void Start()
    {
        button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(SwitchCamera);
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
    }

    void SwitchCamera()
    {
        currentCamera = Camera.main;
        currentCamera.enabled = false;
        cameraToSwitch.enabled = true;

        if (forPlanet)
        {
            Hide(star);
            Show(satellite);
            Show(planet);
        }
        else if (forSatellite)
        {
            Hide(planet);
            Hide(star);
            Show(satellite);
        }
        else if (forStar)
        {
            Hide(planet);
            Hide(satellite);
            Show(star);
        }
        canvas.worldCamera = cameraToSwitch;
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
