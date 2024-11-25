using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedMarksToggle : MonoBehaviour
{
    public Toggle toggle;
    private List<GameObject> redSpheres = new List<GameObject>(); // Persistent list
    private bool isActive = true;

    void Start()
    {
        // Find all red spheres initially and store them persistently
        GameObject[] foundSpheres = GameObject.FindGameObjectsWithTag("RedSphere");
        redSpheres.AddRange(foundSpheres);

        // Attach the toggle listener
        toggle.isOn = true; // Default toggle state
        toggle.onValueChanged.AddListener(ToggleValueChanged);

        // Start a coroutine to keep checking for newly created spheres
        StartCoroutine(CheckForNewSpheres());
    }

    // Coroutine to periodically add newly created spheres to the persistent list
    IEnumerator CheckForNewSpheres()
    {
        while (true)
        {
            GameObject[] foundSpheres = GameObject.FindGameObjectsWithTag("RedSphere");
            foreach (GameObject sphere in foundSpheres)
            {
                if (!redSpheres.Contains(sphere))
                {
                    redSpheres.Add(sphere);
                }
            }
            yield return new WaitForSeconds(1f); // Adjust frequency as needed
        }
    }

    void Update()
    {
        // if the main camera is CAM Planet always deactivate the red spheres
        if (Camera.main.name == "CAM Planet")
        {
            SetSpheresActive(false);
            toggle.isOn = false;
        }
    }

    // Toggle value changed callback
    void ToggleValueChanged(bool value)
    {
        isActive = value;
        SetSpheresActive(isActive);
    }

    // Activate or deactivate the spheres using the persistent list
    void SetSpheresActive(bool isActive)
    {
        foreach (GameObject sphere in redSpheres)
        {
            if (sphere != null)
            {
                sphere.SetActive(isActive);
            }
        }
    }
}
