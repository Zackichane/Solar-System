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
    // Start is called before the first frame update
    void Start()
    {
        // set a player pref for the infinite mode
        
        if (PlayerPrefs.GetInt("infinite") == 1)
        {
            // start a coroutine of 4 seconds to wait the assets to generate
            StartCoroutine(HandleInfiniteMode());
        }
        else
        {
            StartCoroutine(NormalLoadingScreen());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
                Debug.Log("Habitable planet found");
                canvas.GetComponent<Canvas>().enabled = false;
                yield break;
            }
        }

        if (!stopInfinite)
        {
            Debug.Log("No habitable planet found : Try no" + PlayerPrefs.GetInt("nInfinite"));
            text.text = "No habitable planet found : Try no " + PlayerPrefs.GetInt("nInfinite");
            int nInfinite = PlayerPrefs.GetInt("nInfinite");
            nInfinite++;
            PlayerPrefs.SetInt("nInfinite", nInfinite);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Generator");
            canvas.GetComponent<Canvas>().enabled = false;
        }
    }

    IEnumerator NormalLoadingScreen()
    {
        yield return new WaitForSeconds(3);
        Camera.main.GetComponent<Camera>().enabled = false;
        GameObject.Find("Main Camera").GetComponent<Camera>().enabled = true;
        canvas.GetComponent<Canvas>().enabled = false;
    }
}
