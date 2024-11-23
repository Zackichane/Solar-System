using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueMarksToggle : MonoBehaviour
{
    public Toggle toggle;
    private List<GameObject> blueSpheres = new List<GameObject>(); // Persistent list
    private bool isActive = true;

    void Start()
    {
        // Find all blue spheres initially and store them persistently
        GameObject[] foundSpheres = GameObject.FindGameObjectsWithTag("BlueSphere");
        blueSpheres.AddRange(foundSpheres);

        Debug.Log("Initial BlueSpheres Count: " + blueSpheres.Count);

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
            GameObject[] foundSpheres = GameObject.FindGameObjectsWithTag("BlueSphere");
            foreach (GameObject sphere in foundSpheres)
            {
                if (!blueSpheres.Contains(sphere))
                {
                    blueSpheres.Add(sphere);
                    Debug.Log("New Sphere Added: " + sphere.name);
                }
            }
            yield return new WaitForSeconds(1f); // Adjust frequency as needed
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
        foreach (GameObject sphere in blueSpheres)
        {
            if (sphere != null)
            {
                sphere.SetActive(isActive);
            }
        }
        Debug.Log("Spheres " + (isActive ? "Activated" : "Deactivated"));
    }
}
