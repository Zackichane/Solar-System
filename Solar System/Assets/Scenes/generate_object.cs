using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Reference to the prefab (drag and drop in Inspector)
    public GameObject objectToSpawn;

    // Spawn position
    public Vector3 spawnPosition = new Vector3(10, 0, 0);

    void Update()
    {
        // Spawn the object when right clicking
        if (Input.GetMouseButtonDown(1))
        {
            // Instantiate the object at the given position and rotation
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
