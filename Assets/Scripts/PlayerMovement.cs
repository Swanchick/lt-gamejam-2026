using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] public float rotationSpeed = 10f;
    [SerializeField] private float speedMultiplier = 1f;

    private CharacterController controller;
    private GameObject playerObj;
    private Vector3 lastMoveDirection = Vector3.forward;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerObj = this.gameObject;
    }

    void Update()
    {
        if (playerObj.GetComponent<Dance>().isDancing)
        {
            return;
        }

        HandleMovement();
    }

    private void HandleMovement()
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
    }
}
