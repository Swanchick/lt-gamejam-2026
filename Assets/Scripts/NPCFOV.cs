using UnityEngine;

public class NPCFOV : MonoBehaviour
{
    [SerializeField] private float viewAndgle = 110f;
    [SerializeField] private float viewDistance = 5f;

    public bool CanSeePlayer(Transform playerTransform)
    {
        Vector3 dirToPlayer = playerTransform.position - transform.position;
        dirToPlayer.y = 0f;
        
        if (dirToPlayer.magnitude > viewDistance)
        {
            return false;
        }
        
        float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);
        if (angleToPlayer > viewAndgle / 2f)
        {
            return false;
        }
        
        return true;
    }
}
