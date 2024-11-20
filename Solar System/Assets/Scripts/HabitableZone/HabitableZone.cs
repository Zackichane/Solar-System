using UnityEngine;

public class HabitableZone : MonoBehaviour
{
    public SimpleRingGenerator simpleRingGenerator;  // Reference to the SimpleRingGenerator

    private bool luminosityCalculated = false;
    public static float outerHabitableZone;

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
        float innerHabitableZone = Mathf.Sqrt(luminosity / 1.1f) * 1000;  // Adjusted for solar luminosity
        float outerHabitableZone = Mathf.Sqrt(luminosity / 0.53f) * 1000; // Adjusted for solar luminosity

        PlayerPrefs.SetInt("outerHabitableZoneToShow", (int)outerHabitableZone);

        // Log the results for the habitable zone
        Debug.Log($"Habitable Zone (Inner): {innerHabitableZone} Km/scale");
        Debug.Log($"Habitable Zone (Outer): {outerHabitableZone} Km/scale");

        // Call UpdateRing on the SimpleRingGenerator to update the ring
        if (simpleRingGenerator != null)
        {
            simpleRingGenerator.UpdateRing(innerHabitableZone, outerHabitableZone);
        }
    }
}
