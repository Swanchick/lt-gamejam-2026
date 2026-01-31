using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float killRange = 1.5f;

    private SuspicionManager suspicionManager;

    void Start()
    {
        suspicionManager = FindObjectOfType<SuspicionManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryKill();
        }
    }

    private void TryKill()
    {
        KillableNPC target = FindClosestKillableNPC();
        if (target == null) return;

        target.Kill();

        bool seenByAnyone = suspicionManager.IsPlayerSeenByAnyNPC();

        if (seenByAnyone)
            suspicionManager.AddSuspicion(100f);
        else
            suspicionManager.AddSuspicion(30f);
    }

    private KillableNPC FindClosestKillableNPC()
    {
        GameObject[] npcObjects = GameObject.FindGameObjectsWithTag("Enemy");

        KillableNPC closest = null;
        float minDistance = float.MaxValue;

        foreach (GameObject npcObj in npcObjects)
        {
            KillableNPC killable = npcObj.GetComponent<KillableNPC>();
            if (killable == null) continue;

            float dist = Vector3.Distance(transform.position, npcObj.transform.position);
            if (dist <= killRange && dist < minDistance)
            {
                minDistance = dist;
                closest = killable;
            }
        }

        return closest;
    }
}
