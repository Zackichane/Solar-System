using UnityEngine;
using TMPro;

public class RandomTextGenerator : MonoBehaviour
{
    // Array of predefined texts
    [TextArea] // Allows multiline input in the Inspector
    public string[] predefinedTexts;

    // Reference to the TextMeshPro component
    public TextMeshProUGUI textMesh;

    private void Start()
    {
        if (predefinedTexts.Length == 0)
        {
            Debug.LogWarning("No predefined texts provided!");
            return;
        }

        if (textMesh == null)
        {
            Debug.LogError("TextMeshProUGUI component is not assigned!");
            return;
        }

        // Select a random text from the array
        string randomText = predefinedTexts[Random.Range(0, predefinedTexts.Length)];

        if (PlayerPrefs.GetInt("starType") >= 5)
        {
            randomText = PlayerPrefs.GetString("starName");
        }

        // Display the random text in the TextMeshPro box
        textMesh.text = randomText;
    }
}