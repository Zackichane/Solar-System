using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Required for changing scenes

public class ButtonMenu : MonoBehaviour
{
    // Public variable to set the scene name in the Inspector
    public string sceneToLoad;
    public int starType;

    // This function will be called when the object is clicked as a button
    private void OnMouseUpAsButton()
    {
        Debug.Log("Button clicked. Loading scene: " + sceneToLoad);

        // Load the scene based on the name set in the Inspector
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            PlayerPrefs.SetInt("starType", starType);
            PlayerPrefs.Save();
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is empty! Please set a valid scene name in the Inspector.");
        }
    }
}
