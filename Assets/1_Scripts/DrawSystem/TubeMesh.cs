using UnityEngine;
using System.Collections.Generic;

public class TubeMesh : MonoBehaviour
{
    // The diameter of the tube
    public float diameter = .3f;

    // The number of segments per curve
    public int segments = 10;

    // The number of points per segment
    public int pointsPerSegment = 80;

    // The mesh object
    private Mesh mesh;

    // The mesh filter component
    private MeshFilter meshFilter;

    // Use this for initialization
    void Start()
    {
        // Create a new mesh and assign it to the mesh filter
        

    }

    // Generate the tube mesh
    public void GenerateTubeMesh(List<Vector3> points, float width)
    {
        diameter = width;


        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        // Check if the points list is valid
        if (points == null || points.Count < 2)
        {
            Debug.LogWarning("Invalid points list");
            return;
        }

        // Calculate the number of curves
        int curves = (points.Count - 1) / 3;

        // Calculate the number of vertices and triangles
        int vertices = (curves * segments + 1) * pointsPerSegment;
        int triangles = curves * segments * pointsPerSegment * 6;

        // Initialize the arrays
        Vector3[] verts = new Vector3[vertices];
        Vector3[] norms = new Vector3[vertices];
        int[] tris = new int[triangles];

        // Loop through each curve
        for (int i = 0; i < curves; i++)
        {
            // Get the control points for the curve
            Vector3 p0 = points[i * 3];
            Vector3 p1 = points[i * 3 + 1];
            Vector3 p2 = points[i * 3 + 2];
            Vector3 p3 = points[i * 3 + 3];

            // Loop through each segment
            for (int j = 0; j <= segments; j++)
            {
                // Calculate the parameter t
                float t = (float)j / segments;

                // Calculate the point on the curve using Catmull-Rom spline formula
                Vector3 point = 0.5f * ((2 * p1) + (-p0 + p2) * t + (2 * p0 - 5 * p1 + 4 * p2 - p3) * t * t + (-p0 + 3 * p1 - 3 * p2 + p3) * t * t * t);

                // Calculate the tangent vector on the curve
                Vector3 tangent = 0.5f * (-p0 + p2 + 2 * (-p0 + p1) * t + (-p0 + p1 - p2 + p3) * t * t);

                // Calculate the normal vector on the curve using Frenet-Serret formula
                Vector3 normal;
                if (i == 0 && j == 0) // Use the first point as the initial normal
                {
                    normal = Vector3.Cross(tangent, points[0] - points[1]).normalized;
                }
                else // Use the previous normal to calculate the next one
                {
                    normal = Vector3.Cross(norms[(i * segments + j - 1) * pointsPerSegment], tangent).normalized;
                }

                // Calculate the binormal vector on the curve
                Vector3 binormal = Vector3.Cross(tangent, normal).normalized;

                // Loop through each point per segment
                for (int k = 0; k < pointsPerSegment; k++)
                {
                    // Calculate the angle
                    float angle = (float)k / pointsPerSegment * 2 * Mathf.PI;

                    // Calculate the vertex position
                    Vector3 vertex = point + normal * Mathf.Cos(angle) * diameter / 2 + binormal * Mathf.Sin(angle) * diameter / 2;

                    // Add the vertex position and normal to the arrays
                    verts[(i * segments + j) * pointsPerSegment + k] = vertex;
                    norms[(i * segments + j) * pointsPerSegment + k] = normal;
                }
            }
        }

        // Loop through each curve
        for (int i = 0; i < curves; i++)
        {
            // Loop through each segment
            for (int j = 0; j < segments; j++)
            {
                // Loop through each point per segment
                for (int k = 0; k < pointsPerSegment; k++)
                {
                    // Calculate the indices for the quad
                    int i0 = (i * segments + j) * pointsPerSegment + k;
                    int i1 = (i * segments + j) * pointsPerSegment + (k + 1) % pointsPerSegment;
                    int i2 = (i * segments + j + 1) * pointsPerSegment + (k + 1) % pointsPerSegment;
                    int i3 = (i * segments + j + 1) * pointsPerSegment + k;

                    // Add the indices for the two triangles to the array
                    tris[(i * segments + j) * pointsPerSegment * 6 + k * 6] = i0;
                    tris[(i * segments + j) * pointsPerSegment * 6 + k * 6 + 1] = i1;
                    tris[(i * segments + j) * pointsPerSegment * 6 + k * 6 + 2] = i2;
                    tris[(i * segments + j) * pointsPerSegment * 6 + k * 6 + 3] = i2;
                    tris[(i * segments + j) * pointsPerSegment * 6 + k * 6 + 4] = i3;
                    tris[(i * segments + j) * pointsPerSegment * 6 + k * 6 + 5] = i0;
                }
            }
        }

        // Assign the arrays to the mesh
        mesh.vertices = verts;
        mesh.normals = norms;
        mesh.triangles = tris;

        // Recalculate the bounds and normals of the mesh
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
