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

        Vector3[] vertices = new Vector3[segments * 4]; // 4 vertices per segment (front & back)
        int[] triangles = new int[segments * 12]; // 6 triangles per segment (front & back)
        Vector2[] uvs = new Vector2[vertices.Length]; // UV mapping array

        float angleStep = 2 * Mathf.PI / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleStep;
            float x = Mathf.Cos(angle);
            float z = Mathf.Sin(angle);

            // Outer vertex (front)
            vertices[i * 2] = new Vector3(x * outerRadius, 0f, z * outerRadius);
            uvs[i * 2] = new Vector2(i / (float)segments, 1);  // UVs for outer front

            // Inner vertex (front)
            vertices[i * 2 + 1] = new Vector3(x * innerRadius, 0f, z * innerRadius);
            uvs[i * 2 + 1] = new Vector2(i / (float)segments, 0);  // UVs for inner front

            // Outer vertex (back)
            vertices[(segments * 2) + i * 2] = new Vector3(x * outerRadius, 0f, z * outerRadius);
            uvs[(segments * 2) + i * 2] = new Vector2(i / (float)segments, 1);  // UVs for outer back

            // Inner vertex (back)
            vertices[(segments * 2) + i * 2 + 1] = new Vector3(x * innerRadius, 0f, z * innerRadius);
            uvs[(segments * 2) + i * 2 + 1] = new Vector2(i / (float)segments, 0);  // UVs for inner back

            // Front face triangles
            if (i < segments - 1)
            {
                triangles[i * 6] = i * 2;
                triangles[i * 6 + 1] = i * 2 + 1;
                triangles[i * 6 + 2] = i * 2 + 2;

                triangles[i * 6 + 3] = i * 2 + 1;
                triangles[i * 6 + 4] = i * 2 + 3;
                triangles[i * 6 + 5] = i * 2 + 2;
            }
            else
            {
                triangles[i * 6] = i * 2;
                triangles[i * 6 + 1] = i * 2 + 1;
                triangles[i * 6 + 2] = 0;

                triangles[i * 6 + 3] = i * 2 + 1;
                triangles[i * 6 + 4] = 1;
                triangles[i * 6 + 5] = 0;
            }

            // Back face triangles (inverted winding)
            if (i < segments - 1)
            {
                triangles[segments * 6 + i * 6] = (segments * 2) + i * 2;
                triangles[segments * 6 + i * 6 + 1] = (segments * 2) + i * 2 + 2;
                triangles[segments * 6 + i * 6 + 2] = (segments * 2) + i * 2 + 1;

                triangles[segments * 6 + i * 6 + 3] = (segments * 2) + i * 2 + 1;
                triangles[segments * 6 + i * 6 + 4] = (segments * 2) + i * 2 + 2;
                triangles[segments * 6 + i * 6 + 5] = (segments * 2) + i * 2 + 3;
            }
            else
            {
                triangles[segments * 6 + i * 6] = (segments * 2) + i * 2;
                triangles[segments * 6 + i * 6 + 1] = (segments * 2) + 0;
                triangles[segments * 6 + i * 6 + 2] = (segments * 2) + i * 2 + 1;

                triangles[segments * 6 + i * 6 + 3] = (segments * 2) + i * 2 + 1;
                triangles[segments * 6 + i * 6 + 4] = (segments * 2) + 0;
                triangles[segments * 6 + i * 6 + 5] = (segments * 2) + 1;
            }
        }

        ringMesh.vertices = vertices;
        ringMesh.triangles = triangles;
        ringMesh.uv = uvs;  // Assign UVs

        ringMesh.RecalculateNormals(); // Ensure normals are calculated for proper lighting

        meshFilter.mesh = ringMesh;

        Debug.Log("Ring generated with " + segments + " segments.");
    }
}
