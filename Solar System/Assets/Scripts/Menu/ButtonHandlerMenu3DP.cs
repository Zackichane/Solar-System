using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Required for changing scenes

public class ButtonHandlerMenu3DP : MonoBehaviour
{
    public int minPlanet;
    public int maxPlanet;
    public GameObject button;

    void Start()
    {
        button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnMouseUpAsButton);
    }


    private void OnMouseUpAsButton()
    {
        PlayerPrefs.SetInt("minPlanet", minPlanet);
        PlayerPrefs.SetInt("maxPlanet", maxPlanet);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Generator");
    }
    
}
