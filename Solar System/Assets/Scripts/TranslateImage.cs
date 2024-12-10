using UnityEngine;

public class TranslateImage : MonoBehaviour
{
    public float translationSpeed = 1f; // Speed of translation (adjustable)
    public float maxTranslationDistance = 45f; // Max translation distance (adjustable)
    public float startPositionY = 0f; // Starting Y position (adjustable)
    
    private RectTransform rectTransform;

    void Start()
    {
        // Get the RectTransform of the RawImage
        rectTransform = GetComponent<RectTransform>();
        
        // Set the initial position of the image to the startPositionY value
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, startPositionY, rectTransform.localPosition.z);
    }

    void Update()
    {
        // Calculate the oscillation using Mathf.Sin for smooth easing
        float translationAmount = Mathf.Sin(Time.time * translationSpeed) * maxTranslationDistance;

        // Apply the translation to the Y-axis, starting from the specified startPositionY
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, startPositionY + translationAmount, rectTransform.localPosition.z);
    }
}