using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiningWheel : MonoBehaviour
{
    public float rotationSpeed = 200f;
    public Camera mainCamera;
    public Camera loadingScreenCamera;
    public new Light light;

    // start a coroutine for 3 s
    void Start()
    {
        StartCoroutine(WaitForSeconds());
        // disable the light
        light.enabled = false;
    }

    void Update()
    {
        transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(2f);

        mainCamera.enabled = true;
        loadingScreenCamera.enabled = false;
        light.enabled = true;
    }
}
