using UnityEngine;
using TMPro;

public class StarTypeDisplay : MonoBehaviour
{
    public TextMeshProUGUI starTypeText; // Reference to the TextMeshPro UI Text

    void Start()
    {
        // Get the star type from PlayerPrefs (set by StarGeneration script)
        int starType = PlayerPrefs.GetInt("starType");
        string starName;

        // Determine the star type
        switch (starType)
        {
            case 1:
                starName = "White Dwarf";
                break;
            case 2:
                starName = "Yellow Dwarf";
                break;
            case 3:
                starName = "Red Dwarf";
                break;
            case 4:
                starName = "Blue Dwarf";
                break;
            case 5:
                starName = "White Dwarf";
                break;
            case 6:
                starName = "White Dwarf";
                break;
            case 7:
                starName = "Red Dwarf";
                break;
            case 8:
                starName = "Red Dwarf";
                break;
            case 9:
                starName = "Red Dwarf";
                break;
            case 10:
                starName = "Yellow Dwarf";
                break;
            case 11:
                starName = "Yellow Dwarf";
                break;
            case 12:
                starName = "Yellow Dwarf";
                break;
            default:
                starName = "Unknown";
                break;
        }

        // Set the star type name in the TextMeshPro text
        starTypeText.text = starName;
    }
}
