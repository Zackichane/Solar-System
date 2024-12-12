using UnityEngine;

public class Rotate3DModel : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float maxRotationAngle = 45f;
    public float startXRotation = 0f;
    public float startYRotation = 0f;
    public float startZRotation = 0f;

    private Transform modelTransform;

    void Start()
    {
        modelTransform = GetComponent<Transform>();

        modelTransform.localRotation = Quaternion.Euler(startXRotation, startYRotation, startZRotation);
    }

    void Update()
    {
        float rotationAmount = Mathf.Sin(Time.time * rotationSpeed) * maxRotationAngle;

        modelTransform.localRotation = Quaternion.Euler(startXRotation, startYRotation + rotationAmount, startZRotation);
    }
}