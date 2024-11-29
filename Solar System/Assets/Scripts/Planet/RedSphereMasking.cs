using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSphereMasking : MonoBehaviour
{
    private GameObject[] planets;
    
    void Start()
    {
        // start a coroutine of 2s to wait for the planets to be generated
        StartCoroutine(WaitForPlanets());
    }
    void Update()
    {
        if (planets != null && planets.Length > 0)
        {
            for (int i = 0; i < planets.Length; i++)
            {
                if (planets[i].GetComponent<MeshRenderer>().enabled == false)
                {
                    planets[i].GetComponent<planetInfos>().redSphere.SetActive(false);
                }
            }
        }
    }
    IEnumerator WaitForPlanets()
    {
        yield return new WaitForSeconds(2);
        planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
    }
}
