using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetUIManager : MonoBehaviour
{
    public TextMeshProUGUI planetName;
    public TextMeshProUGUI planetType;
    public TextMeshProUGUI planetRadius;
    public TextMeshProUGUI planetTemperature;
    public TextMeshProUGUI planetMass;
    public TextMeshProUGUI distPlanetStar;
    public TextMeshProUGUI planetHabitable;

    private GameObject[] planets;

    private GameObject actualPlanet;

    // Start is called before the first frame update
    void Start()
    {
        // create a coroutine that waits 2 s to get all planets by tag
        StartCoroutine(GetAllPlanets());
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.name == "CAM Planet" && planets != null)
        {

            // iterate over all planets to get the one with the active mesh renderer
            foreach (GameObject planet in planets)
            {
                if (planet != null && planet.GetComponent<MeshRenderer>().enabled)
                {
                    // get the planet's data and display it
                    planetName.text = "Planet Name : " + planet.GetComponent<planetInfos>().planetName;
                    planetType.text = "Planet Type : " + planet.GetComponent<planetInfos>().planetType;
                    planetRadius.text = "Planet Radius : " + planet.GetComponent<planetInfos>().planetRadius + " km";
                    planetTemperature.text = "Planet Temperature : " + planet.GetComponent<planetInfos>().planetTemperature + " K";
                    planetMass.text = "Planet Mass : " + planet.GetComponent<planetInfos>().planetMass + " kg";
                    distPlanetStar.text = "Distance to Star : " + planet.GetComponent<planetInfos>().distPlanetStar + " km";
                    planetHabitable.text = "Habitable : " + planet.GetComponent<planetInfos>().planetHabitable;
                    actualPlanet = planet;
                }
            }
        }
    }

    IEnumerator GetAllPlanets()
    {
        yield return new WaitForSeconds(2.5f);
        planets = GameObject.FindGameObjectsWithTag("GeneratedPlanet");
        //actualPlanet = planets[0];
    }
}
