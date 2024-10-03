using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbiteManager : MonoBehaviour
{
    private GameObject[] planets;
    void Start()
    {
        GetAllPlanets();
    }

    void Update()
    {
        if (planets == null)
        {
            GetAllPlanets();
        }
    }

    void GetAllPlanets()
    {
        planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
        // order the planets by scale
        System.Array.Sort(planets, (x, y) => x.transform.localScale.x.CompareTo(y.transform.localScale.x));
    }
}
