using UnityEngine;

public class TranslateImageX : MonoBehaviour
{
    public float translationSpeed = 1f; // Speed of translation (adjustable)
    public float maxTranslationDistance = 45f; // Max translation distance (adjustable)
    public float startPositionX = 0f; // Starting X position (adjustable)
    
    private RectTransform rectTransform;

    void Start()
    {
        // Get the RectTransform of the RawImage
        rectTransform = GetComponent<RectTransform>();
        
        // Set the initial position of the image to the startPositionX value
        rectTransform.localPosition = new Vector3(startPositionX, rectTransform.localPosition.y, rectTransform.localPosition.z);
    }

    void Update()
    {
        // Calculate the oscillation using Mathf.Sin for smooth easing
        float translationAmount = Mathf.Sin(Time.time * translationSpeed) * maxTranslationDistance;

        // Apply the translation to the X-axis, starting from the specified startPositionX
        rectTransform.localPosition = new Vector3(startPositionX + translationAmount, rectTransform.localPosition.y, rectTransform.localPosition.z);
    }
}