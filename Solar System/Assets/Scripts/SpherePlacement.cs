using UnityEngine;

public class SpherePlacement : MonoBehaviour
{
    [Header("Settings")]
    public GameObject prefab;  // The GameObject to place
    public int numberOfObjects = 100;  // Total number of objects to place
    public float sphereRadius = 5f;  // Maximum radius of the sphere

    void Start()
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab is not assigned!");
            return;
        }

        PlaceObjectsInSphere();
    }

    void PlaceObjectsInSphere()
    {
        Vector3 sphereCenter = transform.position; // Center of the sphere (GameObject's position)

        for (int i = 0; i < numberOfObjects; i++)
        {
            // Generate a random radius (distance from the center), allowing placement inside the sphere
            float randomRadius = Random.Range(0f, sphereRadius);

            // Generate random spherical coordinates
            float theta = Random.Range(0f, Mathf.PI * 2f);  // Azimuthal angle
            float phi = Mathf.Acos(Random.Range(-1f, 1f));  // Polar angle

            // Convert spherical coordinates to Cartesian coordinates
            float x = randomRadius * Mathf.Sin(phi) * Mathf.Cos(theta);
            float y = randomRadius * Mathf.Sin(phi) * Mathf.Sin(theta);
            float z = randomRadius * Mathf.Cos(phi);

            // Calculate the final position relative to the sphere's center
            Vector3 position = sphereCenter + new Vector3(x, y, z);

            // Instantiate the object at the calculated position
            Instantiate(prefab, position, Quaternion.identity, transform);
        }
    }
}
