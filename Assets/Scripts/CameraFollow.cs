using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 10, -10);
    public float smoothSpeed = 10f;

    void Update()
    {
        if (!target) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.LookAt(target);
    }
}
