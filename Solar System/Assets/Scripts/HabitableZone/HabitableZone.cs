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
        float innerHabitableZone = (float)(Mathf.Sqrt(luminosity / 1.1f) * 1.496e+8);  // Adjusted for solar luminosity
        outerHabitableZone = (float)(Mathf.Sqrt(luminosity / 0.53f) * 1.496e+8); // Adjusted for solar luminosity

        innerHabitableZone /= 10000; // Convert to Km/scale
        outerHabitableZone /= 10000; // Convert to Km/scale

        //PlayerPrefs.SetInt("outerHabitableZoneToShow", (int)outerHabitableZone);

        // Log the results for the habitable zone

        // Call UpdateRing on the SimpleRingGenerator to update the ring
        if (simpleRingGenerator != null)
        {
            simpleRingGenerator.UpdateRing(innerHabitableZone, outerHabitableZone);
        }
    }
}
