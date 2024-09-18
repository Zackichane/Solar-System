using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1;  // First camera
    public Camera camera2;  // Second camera
    public GameObject objectToSpawn;  // Object to spawn when C is pressed
    public float spawnDistance = 2f;  // Distance at which object spawns in front of the active camera

    private bool isCamera1Active = true;  // Track which camera is active

    void Start()
    {
        // Ensure only the first camera is active at the start
        camera1.enabled = true;
        camera2.enabled = false;

        // Ensure camera movement control for camera1 is active at the start
        camera1.GetComponent<CameraController>().enabled = true;
        camera2.GetComponent<CameraController>().enabled = false;
    }

    void Update()
    {
        // Switch cameras when R is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            isCamera1Active = !isCamera1Active;  // Toggle active camera

            // Enable the active camera, disable the other one
            camera1.enabled = isCamera1Active;
            camera2.enabled = !isCamera1Active;

            // Toggle movement control based on which camera is active
            camera1.GetComponent<CameraController>().enabled = isCamera1Active;
            camera2.GetComponent<CameraController>().enabled = !isCamera1Active;
        }

        // Spawn object only with the active camera when C is pressed
        if (Application.isPlaying && Input.GetKeyDown(KeyCode.C))
        {
            if (isCamera1Active)
            {
                // Spawn object in front of camera1
                SpawnObject(camera1);
            }
            else
            {
                // Spawn object in front of camera2
                SpawnObject(camera2);
            }
        }
    }

    void SpawnObject(Camera activeCamera)
    {
        // Ensure objectToSpawn is assigned
        if (objectToSpawn != null)
        {
            // Calculate the spawn position in front of the active camera
            Vector3 spawnPosition = activeCamera.transform.position + activeCamera.transform.forward * spawnDistance;
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No object assigned to 'objectToSpawn'!");
        }
    }
}
