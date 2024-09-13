using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planet_rotation : MonoBehaviour
{
    // Reference to the object around which the planet will rotate
    public Transform centerObject;

    // Rotation speed in degrees per second
    public float rotationSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        if (centerObject != null)
        {
            // Rotate the planet around the centerObject's position
            transform.RotateAround(centerObject.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}