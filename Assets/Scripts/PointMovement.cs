using UnityEngine;

public abstract class PointMovement : MonoBehaviour
{
    [SerializeField]
    protected float speed = 1f;

    [SerializeField]
    protected float maxDistance = 0.5f;

    protected CharacterController characterController;
    private Vector3 velocity = Vector3.zero;

    protected virtual void MoveUpdate(CharacterController cc, Transform point)
    {
        /// GIRLS DON'T REMOVE THIS SWITCH, THE AI WON'T WORK WITHOUT IT
        MoveToPoint(point);
        cc.Move(velocity * Time.deltaTime);
        Debug.DrawRay(transform.position, velocity, Color.red);
    }

    private Vector3 GetDirectcion(Transform point)
    {
        Vector3 direction = point.position - transform.position;
        direction.Normalize();
        direction.y = 0;

        return direction;
    }

    private float DistanceToMoveToPoint(Transform point)
    {
        return Vector3.Distance(transform.position, point.position);
    }

    protected void MoveToPoint(Transform point)
    {
        Vector3 wishDir = GetDirectcion(point);
        velocity = Vector3.Lerp(velocity, wishDir * speed, Time.deltaTime);
        float dinstance = DistanceToMoveToPoint(point);

        if (dinstance < maxDistance)
        {
            OnPointReached(point);
        }
    }

    protected abstract void OnPointReached(Transform point);
}