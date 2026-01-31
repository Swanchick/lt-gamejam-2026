using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class FOVMesh : MonoBehaviour
{
    private FOVLogic npcFOV;
    [SerializeField] private int segments = 20;

    private Mesh mesh;

    void Awake()
    {
        if (!npcFOV)
            npcFOV = GetComponentInParent<FOVLogic>();

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void Start()
    {
        RebuildMesh();
    }

    public void RebuildMesh()
    {
        float viewAngle = npcFOV.viewAngle;
        float viewDistance = npcFOV.viewDistance;

        Vector3[] vertices = new Vector3[segments + 2];
        int[] triangles = new int[segments * 3];
        Color[] colors = new Color[vertices.Length];

        vertices[0] = Vector3.zero;
        // Color brightest center
        colors[0] = new Color(1f, 1f, 1f, 0.35f); 

        float angleStep = viewAngle / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = -viewAngle / 2f + angleStep * i;
            Vector3 dir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;

            vertices[i + 1] = dir * viewDistance;

            // Color fade to edge
            float t = (float)i / segments;
            float alpha = Mathf.Lerp(0.35f, 0f, t); 
            colors[i + 1] = new Color(1f, 1f, 1f, alpha);
        }

        int triIndex = 0;
        for (int i = 0; i < segments; i++)
        {
            triangles[triIndex++] = 0;
            triangles[triIndex++] = i + 1;
            triangles[triIndex++] = i + 2;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();
    }
}
