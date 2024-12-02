using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiningWheel : MonoBehaviour
{
    public float rotationSpeed = 200f;
    public Camera mainCamera;
    public Camera loadingScreenCamera;
    public new Light light;

    private GameObject[] planets;
    private bool stopInfinite = false;
    // start a coroutine for 3 s
    void Start()
    {
        StartCoroutine(WaitForSeconds());
        // disable the light
        light.enabled = false;
    }

    void Update()
    {
        transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(3f);

        

        if (PlayerPrefs.GetInt("infinite") == 1)
        {
            StartCoroutine(HandleInfiniteMode());
        }
        else
        {
            mainCamera.enabled = true;
            loadingScreenCamera.enabled = false;
            light.enabled = true;
        }	
    }

    IEnumerator HandleInfiniteMode()
    {
        yield return new WaitForSeconds(1);
        planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
        foreach (GameObject planet in planets)
        {
            string isHabitable = planet.GetComponent<planetInfos>().planetHabitable;
            if (isHabitable == "yes")
            {
                stopInfinite = true;
                Debug.Log("Habitable planet found");

                mainCamera.enabled = true;
                loadingScreenCamera.enabled = false;
                light.enabled = true;
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
