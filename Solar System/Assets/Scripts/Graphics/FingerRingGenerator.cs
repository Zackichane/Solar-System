using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class FingerRingGenerator : MonoBehaviour
{
    public float radius = 0.05f;        // Outer radius of the ring
    public float height = 0.01f;        // Thickness of the band (along the Y-axis)
    public int segments = 64;           // Number of segments (higher = smoother)
    public float finalRingRadius = 0.1f;  // The radius of the final ring

    private Mesh ringMesh;              // Store the generated mesh
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    void Start()
    {
        // Cache the MeshFilter and MeshRenderer components
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        // Set the material to be two-sided (showing both sides of the mesh)
        meshRenderer.material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);

        // Initialize the ringMesh if not already
        if (ringMesh == null)
        {
            ringMesh = new Mesh();
        }

        // Generate the ring mesh
        GenerateRing();
    }

    void GenerateRing()
    {
        if (ringMesh == null)
        {
            ringMesh = new Mesh();  // Initialize if necessary
        }

        Vector3[] outerVertices = new Vector3[segments * 2];  // Outer vertices (top and bottom)
        Vector3[] innerVertices = new Vector3[segments * 2];  // Inner vertices (top and bottom)
        Vector2[] outerUVs = new Vector2[segments * 2];       // Outer UVs
        Vector2[] innerUVs = new Vector2[segments * 2];       // Inner UVs
        int[] outerTriangles = new int[segments * 6];         // Triangles for the outer surface
        int[] innerTriangles = new int[segments * 6];         // Triangles for the inner surface

        float angleStep = 2 * Mathf.PI / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleStep;

            // Outer ring vertices (outer radius)
            float outerX = Mathf.Cos(angle) * finalRingRadius;
            float outerZ = Mathf.Sin(angle) * finalRingRadius;
            outerVertices[i * 2] = new Vector3(outerX, height / 2, outerZ);  // Top
            outerVertices[i * 2 + 1] = new Vector3(outerX, -height / 2, outerZ);  // Bottom

            // Outer ring UVs (based on angle)
            outerUVs[i * 2] = new Vector2(i / (float)segments, 1);  // Top
            outerUVs[i * 2 + 1] = new Vector2(i / (float)segments, 0);  // Bottom

            // Inner ring vertices (inner radius)
            float innerX = Mathf.Cos(angle) * (finalRingRadius - height);  // Shrink radius for inner surface
            float innerZ = Mathf.Sin(angle) * (finalRingRadius - height);
            innerVertices[i * 2] = new Vector3(innerX, height / 2, innerZ);  // Top
            innerVertices[i * 2 + 1] = new Vector3(innerX, -height / 2, innerZ);  // Bottom

            // Inner ring UVs (based on angle)
            innerUVs[i * 2] = new Vector2(i / (float)segments, 1);  // Top
            innerUVs[i * 2 + 1] = new Vector2(i / (float)segments, 0);  // Bottom
        }

        // Outer ring triangles
        for (int i = 0; i < segments; i++)
        {
            int next = (i + 1) % segments;

            // Outer ring
            outerTriangles[i * 6] = i * 2;
            outerTriangles[i * 6 + 1] = next * 2;
            outerTriangles[i * 6 + 2] = i * 2 + 1;

            outerTriangles[i * 6 + 3] = next * 2;
            outerTriangles[i * 6 + 4] = next * 2 + 1;
            outerTriangles[i * 6 + 5] = i * 2 + 1;
        }

        // Inner ring triangles
        for (int i = 0; i < segments; i++)
        {
            int next = (i + 1) % segments;

            // Inner ring
            innerTriangles[i * 6] = i * 2;
            innerTriangles[i * 6 + 1] = next * 2;
            innerTriangles[i * 6 + 2] = i * 2 + 1;

            innerTriangles[i * 6 + 3] = next * 2;
            innerTriangles[i * 6 + 4] = next * 2 + 1;
            innerTriangles[i * 6 + 5] = i * 2 + 1;
        }

        // Combine outer and inner meshes
        Vector3[] combinedVertices = new Vector3[outerVertices.Length + innerVertices.Length];
        Vector2[] combinedUVs = new Vector2[outerUVs.Length + innerUVs.Length];
        int[] combinedTriangles = new int[outerTriangles.Length + innerTriangles.Length];

        // Combine vertices
        outerVertices.CopyTo(combinedVertices, 0);
        innerVertices.CopyTo(combinedVertices, outerVertices.Length);

        // Combine UVs
        outerUVs.CopyTo(combinedUVs, 0);
        innerUVs.CopyTo(combinedUVs, outerUVs.Length);

        // Combine triangles
        for (int i = 0; i < outerTriangles.Length; i++)
        {
            combinedTriangles[i] = outerTriangles[i];
        }

        for (int i = 0; i < innerTriangles.Length; i++)
        {
            combinedTriangles[i + outerTriangles.Length] = innerTriangles[i] + outerVertices.Length / 2;
        }

        // Assign combined vertices, UVs, and triangles to the mesh
        ringMesh.vertices = combinedVertices;
        ringMesh.triangles = combinedTriangles;
        ringMesh.uv = combinedUVs;

        ringMesh.RecalculateNormals();

        // Assign mesh to the MeshFilter
        meshFilter.mesh = ringMesh;
    }

    // Update the ring's thickness and regenerate the ring
    public void UpdateRing(float newRadius, float newHeight, float newFinalRadius)
    {
        if (newRadius != radius || newHeight != height || newFinalRadius != finalRingRadius)
        {
            radius = newRadius;
            height = newHeight;
            finalRingRadius = newFinalRadius;

            GenerateRing();
        }
    }
}