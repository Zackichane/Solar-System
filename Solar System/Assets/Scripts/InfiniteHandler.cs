using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteHandler : MonoBehaviour
{
    private GameObject[] planets;
    private bool stopInfinite = false;
    // Start is called before the first frame update
    void Start()
    {
        // set a player pref for the infinite mode
        
        if (PlayerPrefs.GetInt("infinite") == 1)
        {
            // start a coroutine of 4 seconds to wait the assets to generate
            StartCoroutine(HandleInfiniteMode());
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
                yield break;
            }
        }

        if (!stopInfinite)
        {
            Debug.Log("No habitable planet found : Try no" + PlayerPrefs.GetInt("nInfinite"));
            int nInfinite = PlayerPrefs.GetInt("nInfinite");
            nInfinite++;
            PlayerPrefs.SetInt("nInfinite", nInfinite);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Generator");
        }
    }
}
