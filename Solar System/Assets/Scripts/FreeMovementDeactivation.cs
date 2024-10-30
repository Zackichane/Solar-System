using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovementDeactivation : MonoBehaviour
{
    public GameObject gameObject;
    void Start()
    {
        // add the gameObject to playerprefabs as a gameobject
        PlayerPrefs.SetString("playerprefabs", gameObject.name);
    }

    void Update()
    {
        // get the main camera with is name
        GameObject mainCamera = GameObject.Find("Main Camera");
        // check if it is active
        if (mainCamera.activeSelf)
        {
            // deactivate the toggle object
            gameObject.SetActive(true);
        }
        else
        {
            // activate the toggle object
            gameObject.SetActive(true);
        }
    }
}
