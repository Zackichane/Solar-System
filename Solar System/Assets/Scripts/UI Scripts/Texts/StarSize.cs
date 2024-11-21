using UnityEngine;
using TMPro;
using System.Collections;

public class StarSize : MonoBehaviour
{
    public TextMeshProUGUI sizeText;  // Reference to the TextMeshPro UI Text

    void Start()
    {
        // Start the coroutine to wait until the size is set
        StartCoroutine(WaitForSize());
    }

    IEnumerator WaitForSize()
    {
        // Wait until starSize is greater than 0 (ensuring it has been set by StarGeneration)
        while (StarGeneration.starSize == 0)
        {
            yield return null; // Wait for the next frame
        }

        // Once size is set, display it
        sizeText.text = $"{StarGeneration.starSize * 10000} km"; // Multiply by 10,000 for actual size in km
    }
}
