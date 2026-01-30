using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] public float rotationSpeed = 10f;
    [SerializeField] private float speedMultiplier = 1f;

    private CharacterController controller;
    private Vector3 lastMoveDirection = Vector3.forward;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(horizontal, 0f, vertical);

        if (move.magnitude > 1f)
            move.Normalize();

        // Move
        controller.Move(move * moveSpeed * speedMultiplier * Time.deltaTime);

        // Rotate towards movement direction
        if (move.sqrMagnitude > 0.001f)
        {
            lastMoveDirection = move;
            Quaternion targetRotation = Quaternion.LookRotation(lastMoveDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Lock Y
        transform.position = new Vector3(
            transform.position.x,
            0f,
            transform.position.z
        );
    }
}
