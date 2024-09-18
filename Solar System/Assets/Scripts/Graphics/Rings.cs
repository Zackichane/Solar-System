using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SimpleRingGenerator : MonoBehaviour
{
    public float innerRadius = 1f;  // Inner radius of the ring
    public float outerRadius = 2f;  // Outer radius of the ring
    public int segments = 64;       // Number of segments (higher = smoother)

    void Start()
    {
        GenerateRing();
    }

    void GenerateRing()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh ringMesh = new Mesh();

        Vector3[] vertices = new Vector3[segments * 2];
        int[] triangles = new int[segments * 6];

        float angleStep = 2 * Mathf.PI / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleStep;
            float x = Mathf.Cos(angle);
            float z = Mathf.Sin(angle);

            // Outer vertex
            vertices[i * 2] = new Vector3(x * outerRadius, 0f, z * outerRadius);

            // Inner vertex
            vertices[i * 2 + 1] = new Vector3(x * innerRadius, 0f, z * innerRadius);

            // Set triangles (2 triangles per segment)
            if (i < segments - 1)
            {
                // First triangle
                triangles[i * 6] = i * 2;
                triangles[i * 6 + 1] = i * 2 + 1;
                triangles[i * 6 + 2] = i * 2 + 2;

                // Second triangle
                triangles[i * 6 + 3] = i * 2 + 1;
                triangles[i * 6 + 4] = i * 2 + 3;
                triangles[i * 6 + 5] = i * 2 + 2;
            }
            else
            {
                // Wrap last segment back to the first vertices
                triangles[i * 6] = i * 2;
                triangles[i * 6 + 1] = i * 2 + 1;
                triangles[i * 6 + 2] = 0;

                triangles[i * 6 + 3] = i * 2 + 1;
                triangles[i * 6 + 4] = 1;
                triangles[i * 6 + 5] = 0;
            }
        }

        ringMesh.vertices = vertices;
        ringMesh.triangles = triangles;
        ringMesh.RecalculateNormals();

        meshFilter.mesh = ringMesh;

        Debug.Log("Ring generated with " + segments + " segments.");
    }
}
