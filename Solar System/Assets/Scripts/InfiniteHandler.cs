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
        PlayerPrefs.SetInt("infinite", 1);
        PlayerPrefs.SetInt("nInfinite", 0);
        if (PlayerPrefs.GetInt("infinite") == 1)
        {
            // start a coroutine of 4 seconds to wait the assets to generate
            StartCoroutine(WaitAssets());

            if (stopInfinite)
            {
                // load the Generator scene
                int nInfinite = PlayerPrefs.GetInt("nInfinite");
                nInfinite++;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Generator");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitAssets()
    {
        yield return new WaitForSeconds(4);
        planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
        foreach (GameObject planet in planets)
        {
            string isHabitable = planet.GetComponent<planetInfos>().planetHabitable;
            if (isHabitable == "yes")
            {
                stopInfinite = true;
            }
            else
            // load the Generator scene
            {
                stopInfinite = false;
            }
        }

    }
}
