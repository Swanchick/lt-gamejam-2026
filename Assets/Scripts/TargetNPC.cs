using UnityEngine;

[RequireComponent(typeof(KillableNPC))]
public class TargetNPC : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<KillableNPC>().OnKilled += OnTargetKilled;
    }

    private void OnTargetKilled()
    {
        Debug.Log("Target NPC eliminated. Triggering win sequence.");   
        GameManager.Instance.OnTargetEliminated();
    }
}
