using UnityEngine;
using TMPro;
using System.Collections;

public class StarGenerationTemperature : MonoBehaviour
{
    public TextMeshProUGUI temperatureText;  // Reference to the TextMeshPro UI Text

    void Start()
    {
        // Start the coroutine to wait until the temperature is set
        StartCoroutine(WaitForTemperature());
    }

    IEnumerator WaitForTemperature()
    {
        // Wait until StarTemperature is greater than 0
        while (StarGeneration.starTemperature == 0)
        {
            yield return null; // Wait for the next frame
        }

        // Once temperature is set, display it
        temperatureText.text = $"{StarGeneration.starTemperature} K";

        // Log for debugging
        Debug.Log("Star Temperature from StarGeneration: " + StarGeneration.starTemperature);
    }
}
