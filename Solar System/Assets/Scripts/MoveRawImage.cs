using UnityEngine;

public class RotateImage : MonoBehaviour
{
    public float rotationSpeed = 1f; // Speed of rotation (adjustable)
    public float maxRotationAngle = 45f; // Max rotation angle (adjustable)
    public float startRotation = 0f; // Starting rotation angle (adjustable)
    
    private RectTransform rectTransform;

    void Start()
    {
        // Get the RectTransform of the RawImage
        rectTransform = GetComponent<RectTransform>();
        
        // Set the initial rotation of the image to the startRotation value
        rectTransform.localRotation = Quaternion.Euler(0f, startRotation, 0f);
    }

    void Update()
    {
        // Calculate the oscillation using Mathf.Sin for smooth easing
        float rotationAmount = Mathf.Sin(Time.time * rotationSpeed) * maxRotationAngle;

        // Apply the rotation to the Y-axis, starting from the specified startRotation
        rectTransform.localRotation = Quaternion.Euler(0f, startRotation + rotationAmount, 0f);
    }
}
