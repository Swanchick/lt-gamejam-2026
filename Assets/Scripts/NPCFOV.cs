using UnityEngine;

public class NPCFOV : MonoBehaviour
{
    [SerializeField] public float viewAngle = 110f;
    [SerializeField] public float viewDistance = 5f;

    public bool CanSeePlayer(Transform playerTransform)
    {
        Vector3 dirToPlayer = playerTransform.position - transform.position;
        dirToPlayer.y = 0f;
        
        if (dirToPlayer.magnitude > viewDistance)
        {
            return false;
        }
        
        float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);
        if (angleToPlayer > viewAngle / 2f)
        {
            return false;
        }
        
        Debug.Log("NPC detected player");
        return true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2f, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2f, 0) * transform.forward;

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewDistance);
    }

}
