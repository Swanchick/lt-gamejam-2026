using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 10, -10);
    public float smoothSpeed = 10f;

    private Quaternion initialRotation;

    void Start()
    {
        if (!target) return;

        Vector3 direction = -offset; // camera position relative to target
        initialRotation = Quaternion.LookRotation(direction);
        transform.rotation = initialRotation;
    }

    void Update()
    {
        if (!target) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );
        
        transform.rotation = initialRotation;
    }
}
