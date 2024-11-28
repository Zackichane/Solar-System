using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedMarksToggle : MonoBehaviour
{
    public Toggle toggle_red;
    public Toggle toggle_blue;
    private List<GameObject> redSpheres = new List<GameObject>(); // Persistent list
    private List<GameObject> blueSpheres = new List<GameObject>();
    private Camera currentCamera;

    void Start()
    {
        // Find all red spheres initially and store them persistently
        GameObject[] foundSpheres = GameObject.FindGameObjectsWithTag("RedSphere");
        GameObject[] foundBlueSpheres = GameObject.FindGameObjectsWithTag("BlueSphere");
        redSpheres.AddRange(foundSpheres);
        blueSpheres.AddRange(foundBlueSpheres);

        // RED
        toggle_red.isOn = true; // Default toggle state
        toggle_red.onValueChanged.AddListener(ToggleValueChangedRed);

        // BLUE
        toggle_blue.isOn = true; // Default toggle state
        toggle_blue.onValueChanged.AddListener(ToggleValueChangedBlue);

        // Start a coroutine to keep checking for newly created spheres
        StartCoroutine(CheckForNewSpheres());
    }

    // Coroutine to periodically add newly created spheres to the persistent list
    IEnumerator CheckForNewSpheres()
    {
        while (true)
        {
            // RED
            GameObject[] foundRedSpheres = GameObject.FindGameObjectsWithTag("RedSphere");
            foreach (GameObject sphere in foundRedSpheres)
            {
                if (!redSpheres.Contains(sphere))
                {
                    redSpheres.Add(sphere);
                }
            }

            // BLUE
            GameObject[] foundBlueSpheres = GameObject.FindGameObjectsWithTag("BlueSphere");
            foreach (GameObject sphere in foundBlueSpheres)
            {
                if (!blueSpheres.Contains(sphere))
                {
                    blueSpheres.Add(sphere);
                }
            }

            yield return new WaitForSeconds(1f); // Adjust frequency as needed
        }
    }

    void Update()
    {
        if (currentCamera == null)
        {
            currentCamera = Camera.main;
        }
        if (Camera.main.name == "CAM Planet" && currentCamera.name != "CAM Planet")
        {
            SetRedSphereActive(false);
            toggle_red.isOn = false;
            toggle_red.interactable = false;

            SetBlueSphereActive(true);
            toggle_blue.isOn = true;
            toggle_blue.interactable = true;
            currentCamera = Camera.main;
        }
        if (Camera.main.name == "CAM Satellite" && currentCamera.name != "CAM Satellite")
        {
            SetRedSphereActive(true);
            toggle_red.isOn = true;
            toggle_red.interactable = true;

            SetBlueSphereActive(false);
            toggle_blue.isOn = false;
            toggle_blue.interactable = false;
            currentCamera = Camera.main;
        }
        if (Camera.main.name == "Main Camera" && currentCamera.name != "Main Camera")
        {
            // set everything to default
            SetRedSphereActive(true);
            toggle_red.isOn = true;
            toggle_red.interactable = true;

            SetBlueSphereActive(true);
            toggle_blue.isOn = true;
            toggle_blue.interactable = true;
        }
        if (Camera.main.name == "CAM Star" && currentCamera.name != "CAM Star")
        {
            SetRedSphereActive(true);
            toggle_red.isOn = true;
            toggle_red.interactable = true;

            SetBlueSphereActive(true);
            toggle_blue.isOn = true;
            toggle_blue.interactable = true;
            currentCamera = Camera.main;
        }
        else
        {
            currentCamera = Camera.main;
        }
    }

    // Toggle value changed callback
    void ToggleValueChangedRed(bool isActive)
    {
        isActive = toggle_red.isOn;
        SetRedSphereActive(isActive);
    }
    
    void ToggleValueChangedBlue(bool isActive)
    {
        isActive = toggle_blue.isOn;
        SetBlueSphereActive(isActive);
    }

    // Activate or deactivate the spheres using the persistent list
    void SetRedSphereActive(bool isActive)
    {
        foreach (GameObject sphere in redSpheres)
        {
            if (sphere != null)
            {
                sphere.SetActive(isActive);
            }
        }
    }

    void SetBlueSphereActive(bool isActive)
    {
        foreach (GameObject sphere in blueSpheres)
        {
            if (sphere != null)
            {
                sphere.SetActive(isActive);
            }
        }
    }
}
