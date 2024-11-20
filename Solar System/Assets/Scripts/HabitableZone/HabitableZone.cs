using UnityEngine;

public class HabitableZoneCalculator : MonoBehaviour
{
    private bool luminosityCalculated = false;

    void Update()
    {
        // If Luminosity is calculated and not used yet
        if (!luminosityCalculated && StarGeneration.Luminosity > 0)
        {
            luminosityCalculated = true; // Mark luminosity as calculated
            CalculateHabitableZone(StarGeneration.Luminosity); // Proceed with calculation
        }
    }

    void CalculateHabitableZone(float luminosity)
    {
        // Using the formula for the habitable zone
        float innerHabitableZone = Mathf.Sqrt(luminosity / 1.1f);  // Adjusted for solar luminosity
        float outerHabitableZone = Mathf.Sqrt(luminosity / 0.53f); // Adjusted for solar luminosity

        // Log the results for the habitable zone
        Debug.Log($"Habitable Zone (Inner): {innerHabitableZone} AU");
        Debug.Log($"Habitable Zone (Outer): {outerHabitableZone} AU");
    }
}
