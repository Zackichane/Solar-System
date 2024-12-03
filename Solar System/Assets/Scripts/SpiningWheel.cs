using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiningWheel : MonoBehaviour
{
    private float rotationSpeed = 200f;

    void Update()
    {
        transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
    }
}
