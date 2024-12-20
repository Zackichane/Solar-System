using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfiniteHandler : MonoBehaviour
{
    private GameObject[] planets;
    private bool stopInfinite = false;
    public GameObject canvas;

    public TextMeshProUGUI text;

    public TextMeshProUGUI message;

    public Camera mainCamera;
    public Camera loadingScreenCamera;
    public new Light light;

    private int nTriesToMake = 2000; // Nombre d'essais pour trouver une planète habitable en mode stats
    private bool statsMode = false; // Si statsMode est true, on essai de trouver le nombre de planètes habitables en nTriesToMake essais
    // Start is called before the first frame update
    void Start()
    {
        // set a player pref for the infinite mode
        
        if (PlayerPrefs.GetInt("infinite") == 1)
        {
            // start a coroutine of 4 seconds to wait the assets to generate
            StartCoroutine(HandleInfiniteMode());
            SetText();
        }
        else
        {
            StartCoroutine(NormalLoadingScreen());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (message.enabled)
        {
            // wait 5 seconds then disable the message
            StartCoroutine(DisableMessage());
        }
    }

    IEnumerator HandleInfiniteMode()
    {
        int nInfinite = PlayerPrefs.GetInt("nInfinite");
        Debug.Log("nInfinite : " + nInfinite);
        Debug.Log("nTriesToMake : " + nTriesToMake);
        Debug.Log("statsMode : " + statsMode);

        if (nInfinite >= nTriesToMake && statsMode == true)
        {
            stopInfinite = true;

            Debug.Log("Habitable Planets Found: " + PlayerPrefs.GetInt("nHabitable") + " Tries: " + nInfinite);
            text.text = "Habitable Planets Found: " + PlayerPrefs.GetInt("nHabitable") + " Tries: " + nInfinite;

            yield break;
        }
        yield return new WaitForSeconds(2);
        planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
        foreach (GameObject planet in planets)
        {
            string isHabitable = planet.GetComponent<planetInfos>().planetHabitable;
            if (statsMode == false)
            {
                Debug.Log("checking for habitable planets");
                if (isHabitable == "yes")
                {
                    stopInfinite = true;
                    message.enabled = true;
                    message.text = "Habitable Planet Found! \n It took " + nInfinite + " tries to find it!";

                    loadingScreenCamera.enabled = false;
                    mainCamera.enabled = true;
                    light.enabled = true;
                    canvas.GetComponent<Canvas>().enabled = false;
                    yield break;
                }
            }
            else if (statsMode == true)
            {
                if (isHabitable == "yes")
                {
                    Debug.Log("habitable planet found");
                    int nHabitable = PlayerPrefs.GetInt("nHabitable");
                    nHabitable++;
                    PlayerPrefs.SetInt("nHabitable", nHabitable);
                }
            }
        }

        if (!stopInfinite)
        {
            int currentNInfinite = PlayerPrefs.GetInt("nInfinite");
            currentNInfinite++;
            PlayerPrefs.SetInt("nInfinite", currentNInfinite);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Generator");
            yield break;
        }
    }

    IEnumerator NormalLoadingScreen()
    {
        yield return new WaitForSeconds(2);
        loadingScreenCamera.enabled = false;
        mainCamera.enabled = true;
        light.enabled = true;
        canvas.GetComponent<Canvas>().enabled = false;
    }

    void SetText()
    {   if (!stopInfinite)
        {
            text.text = " Attempt # " + PlayerPrefs.GetInt("nInfinite");
        }
        else if (statsMode == true)
        {
            text.text = "Habitable Planet : " + PlayerPrefs.GetInt("nHabitable") + " Tries: " + PlayerPrefs.GetInt("nInfinite");
        }
    }

    IEnumerator DisableMessage()
    {
        yield return new WaitForSeconds(15);
        message.enabled = false;
    }
}
