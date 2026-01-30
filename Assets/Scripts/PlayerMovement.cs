using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    
    [SerializeField] private float speedMultiplier = 1f;

    private CharacterController controller;

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

        controller.Move(move * moveSpeed * speedMultiplier * Time.deltaTime);

        transform.position = new Vector3(

        transform.position.x,
        0f,
        transform.position.z
    );
    }
}
