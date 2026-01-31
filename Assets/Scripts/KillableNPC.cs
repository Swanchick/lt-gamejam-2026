using UnityEngine;

public class KillableNPC : MonoBehaviour
{
    private Npc npc;
    private CharacterController controller;

    private bool isDead = false;

    void Awake()
    {
        npc = GetComponent<Npc>();
        controller = GetComponent<CharacterController>();
    }

    public void Kill()
    {
        if (isDead) return;
        isDead = true;

        enabled = false;
        npc.enabled = false;

        if (controller != null)
            controller.enabled = false;

        transform.rotation = Quaternion.Euler(90f, transform.eulerAngles.y, 0f);

        // Later: play animation / remove collider / despawn
    }
}