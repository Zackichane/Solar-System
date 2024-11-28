using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSphereMasking : MonoBehaviour
{
    private GameObject[] satellites;
    
    void Start()
    {
        // start a coroutine of 2s to wait for the satellites to be generated
        StartCoroutine(WaitForsatellites());
    }
    void Update()
    {
        if (satellites == null || satellites.Length == 0)
        {
            return;
        }
        for (int i = 0; i < satellites.Length; i++)
        {
            var meshRenderer = satellites[i].GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                satellites[i].GetComponent<planetTracker>().blueSphere.GetComponent<MeshRenderer>().enabled = meshRenderer.enabled;
            }
        }
    }
    IEnumerator WaitForsatellites()
    {
        yield return new WaitForSeconds(2);
        satellites = GameObject.FindGameObjectsWithTag("GeneratedSatellite");
    }
}
