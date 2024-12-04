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
        yield return new WaitForSeconds(4);
        planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
        foreach (GameObject planet in planets)
        {
            string isHabitable = planet.GetComponent<planetInfos>().planetHabitable;
            if (isHabitable == "yes")
            {
                stopInfinite = true;
                message.enabled = true;
                message.text = "Habitable Planet Found! \n It took " + PlayerPrefs.GetInt("nInfinite") + " tries to find it!";
                
                loadingScreenCamera.enabled = false;
                mainCamera.enabled = true;
                light.enabled = true;
                canvas.GetComponent<Canvas>().enabled = false;
                yield break;
            }
        }

        if (!stopInfinite)
        {
            int nInfinite = PlayerPrefs.GetInt("nInfinite");
            nInfinite++;
            PlayerPrefs.SetInt("nInfinite", nInfinite);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Generator");
            yield break;
        }
    }

    IEnumerator NormalLoadingScreen()
    {
        yield return new WaitForSeconds(3);
        loadingScreenCamera.enabled = false;
        mainCamera.enabled = true;
        light.enabled = true;
        canvas.GetComponent<Canvas>().enabled = false;
    }

    void SetText()
    {
        text.text = " Attempt # " + PlayerPrefs.GetInt("nInfinite");
    }

    IEnumerator DisableMessage()
    {
        yield return new WaitForSeconds(15);
        message.enabled = false;
    }
}
