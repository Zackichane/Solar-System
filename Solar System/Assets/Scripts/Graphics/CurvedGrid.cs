using UnityEngine;

public class CurvedGrid : MonoBehaviour
{
    public int width = 20;
    public int height = 20;
    public float curveStrength = 5f;
    public float curveSpeed = 1f;

    private void Start()
    {
        Mesh mesh = CreateCurvedGridMesh(width, height, curveStrength);
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

    Mesh CreateCurvedGridMesh(int width, int height, float curveStrength)
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[(width + 1) * (height + 1)];
        int[] triangles = new int[width * height * 6];
        Vector2[] uv = new Vector2[vertices.Length];

        for (int i = 0, z = 0; z <= height; z++)
        {
            for (int x = 0; x <= width; x++, i++)
            {
                vertices[i] = new Vector3(x, 0, z);

                // Apply curvature to the vertices
                float curve = Mathf.Sin(x * Mathf.PI / width) * curveStrength;
                vertices[i] = new Vector3(vertices[i].x, vertices[i].y + curve, vertices[i].z);
                
                uv[i] = new Vector2((float)x / width, (float)z / height);
            }
        }

        int t = 0;
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                int i0 = z * (width + 1) + x;
                int i1 = (z + 1) * (width + 1) + x;
                int i2 = z * (width + 1) + (x + 1);
                int i3 = (z + 1) * (width + 1) + (x + 1);

                triangles[t++] = i0;
                triangles[t++] = i1;
                triangles[t++] = i2;
                triangles[t++] = i2;
                triangles[t++] = i1;
                triangles[t++] = i3;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        return mesh;
    }
}
