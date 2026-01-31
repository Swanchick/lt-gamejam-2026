using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class FOVVisual : MonoBehaviour
{
    [SerializeField] private Color npcColor = Color.yellow;

    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        ApplyColor();
    }

    private void ApplyColor()
    {
        if (meshRenderer != null && meshRenderer.material != null)
        {
            Color baseColor = meshRenderer.material.color;
            meshRenderer.material.color = new Color(
                npcColor.r,
                npcColor.g,
                npcColor.b,
                baseColor.a
            );
        }
    }
}
