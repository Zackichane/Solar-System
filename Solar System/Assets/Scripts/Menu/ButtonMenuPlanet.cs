using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Required for changing scenes

public class ButtonMenuPlanet : MonoBehaviour
{

    public string sceneToLoad;

    // This function will be called when the object is clicked as a button
    private void OnMouseUpAsButton()
    {
        Debug.Log("Button clicked. Loading scene: " + sceneToLoad);

        // Load the scene based on the name set in the Inspector
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is empty! Please set a valid scene name in the Inspector.");
        }
    }
}