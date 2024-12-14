using UnityEngine;

public class WormholeMesh : MonoBehaviour
{
    public float entranceRadius = 3f; // Radius of the entrance
    public float exitRadius = 1f; // Radius of the exit
    public float length = 10f; // Length of the wormhole (distance between entrance and exit)
    public int segments = 36; // Number of segments around the wormhole
    public int radialSegments = 12; // Number of segments along the length of the wormhole

    private void Start()
    {
        Mesh mesh = CreateWormhole(entranceRadius, exitRadius, length, segments, radialSegments);
        GetComponent<MeshFilter>().mesh = mesh;
    }

    Mesh CreateWormhole(float entranceRadius, float exitRadius, float length, int segments, int radialSegments)
    {
        Mesh mesh = new Mesh();

        int numVertices = radialSegments * segments * 2;
        int numTriangles = radialSegments * segments * 2 * 3;

        Vector3[] vertices = new Vector3[numVertices];
        Vector3[] normals = new Vector3[numVertices];
        Vector2[] uv = new Vector2[numVertices];
        int[] triangles = new int[numTriangles];

        float stepLength = length / radialSegments; // Step size along the length of the wormhole
        float radialStep = 2 * Mathf.PI / segments;

        // Create vertices, normals, and UVs
        for (int i = 0; i < radialSegments; i++)
        {
            float t = (float)i / (radialSegments - 1); // Interpolating between entrance and exit

            float currentRadius = Mathf.Lerp(entranceRadius, exitRadius, t); // Gradually decrease radius from entrance to exit
            float currentZ = i * stepLength;

            for (int j = 0; j < segments; j++)
            {
                float angle = j * radialStep;

                // Vertex positions in a cylindrical shape
                float x = currentRadius * Mathf.Cos(angle);
                float y = currentRadius * Mathf.Sin(angle);
                float z = currentZ;

                int vertexIndex = i * segments + j;
                vertices[vertexIndex] = new Vector3(x, y, z);
                normals[vertexIndex] = new Vector3(x, y, 0).normalized; // Normals facing outward
                uv[vertexIndex] = new Vector2((float)j / segments, t); // UV mapping
            }
        }

        // Create triangles
        int tIndex = 0;
        for (int i = 0; i < radialSegments - 1; i++)
        {
            for (int j = 0; j < segments; j++)
            {
                int nextI = (i + 1) % radialSegments;
                int nextJ = (j + 1) % segments;

                // First triangle (current ring, next ring, next segment)
                triangles[tIndex++] = i * segments + j;
                triangles[tIndex++] = nextI * segments + j;
                triangles[tIndex++] = i * segments + nextJ;

                // Second triangle (next ring, next segment, next segment in current ring)
                triangles[tIndex++] = nextI * segments + j;
                triangles[tIndex++] = nextI * segments + nextJ;
                triangles[tIndex++] = i * segments + nextJ;
            }
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return mesh;
    }
}
