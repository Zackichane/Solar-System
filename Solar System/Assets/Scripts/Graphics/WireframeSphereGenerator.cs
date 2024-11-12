using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class WireframeSphereGenerator : MonoBehaviour
{
    public int longitudeSegments = 24;
    public int latitudeSegments = 16;
    public float radius = 1f;

    void Start()
    {
        GenerateWireframeSphere();
    }

    void GenerateWireframeSphere()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(longitudeSegments + 1) * latitudeSegments + 2];
        int[] indices = new int[longitudeSegments * latitudeSegments * 6];

        int vertIndex = 0;
        float phi, theta;

        // Top pole
        vertices[vertIndex++] = Vector3.up * radius;

        // Create vertices
        for (int lat = 0; lat < latitudeSegments; lat++)
        {
            theta = Mathf.PI * (lat + 1) / (latitudeSegments + 1);
            float sinTheta = Mathf.Sin(theta);
            float cosTheta = Mathf.Cos(theta);

            for (int lon = 0; lon <= longitudeSegments; lon++)
            {
                phi = 2 * Mathf.PI * lon / longitudeSegments;
                float x = Mathf.Cos(phi) * sinTheta;
                float y = cosTheta;
                float z = Mathf.Sin(phi) * sinTheta;
                vertices[vertIndex++] = new Vector3(x, y, z) * radius;
            }
        }

        // Bottom pole
        vertices[vertIndex] = Vector3.down * radius;

        // Set vertices and indices
        mesh.vertices = vertices;
        int index = 0;

        // Generate indices for wireframe
        for (int lat = 0; lat < latitudeSegments; lat++)
        {
            for (int lon = 0; lon < longitudeSegments; lon++)
            {
                int current = lat * (longitudeSegments + 1) + lon + 1;
                int next = current + longitudeSegments + 1;

                // Vertical lines
                indices[index++] = current;
                indices[index++] = next;

                // Horizontal lines
                indices[index++] = current;
                indices[index++] = current + 1;
            }
        }

        // Assign mesh
        mesh.SetIndices(indices, MeshTopology.Lines, 0);
        mesh.RecalculateBounds();
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
