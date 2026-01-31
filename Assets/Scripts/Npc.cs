using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField]
    private GroupMovement group;

    [SerializeField]
    private Transform moveToPoint;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
 
}