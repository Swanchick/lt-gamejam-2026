using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class FOVMesh : MonoBehaviour
{
    public NPCFOV npcFOV;
    public int segments = 20;

    void Start()
    {
        if (!npcFOV)
            npcFOV = GetComponentInParent<NPCFOV>();

        GenerateMesh();
    }

    void GenerateMesh()
    {
        float viewAngle = npcFOV.viewAngle;
        float viewDistance = npcFOV.viewDistance;

        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[segments + 2];
        int[] triangles = new int[segments * 3];

        vertices[0] = Vector3.zero;

        float angleStep = viewAngle / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = -viewAngle / 2 + angleStep * i;
            Vector3 dir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            vertices[i + 1] = dir * viewDistance;
        }

        int triIndex = 0;
        for (int i = 0; i < segments; i++)
        {
            triangles[triIndex++] = 0;
            triangles[triIndex++] = i + 1;
            triangles[triIndex++] = i + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
